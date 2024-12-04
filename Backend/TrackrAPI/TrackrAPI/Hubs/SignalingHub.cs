using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Collections.Concurrent;
using DocumentFormat.OpenXml.Drawing.Charts;
using TrackrAPI.Repositorys.Chats;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SignalingHub : Hub<ISignalingHub>
{
    private readonly IChatPersonaRepository _chatPersonaRepository;

    private static readonly ConcurrentDictionary<string, Queue<Message>> _messageQueues = new ConcurrentDictionary<string, Queue<Message>>();

    private static readonly ConcurrentDictionary<string, string> _peerIds = new ConcurrentDictionary<string, string>();

    private static readonly ConcurrentDictionary<string, string> _connectionIds = new ConcurrentDictionary<string, string>();
    private readonly int _ackTimeoutMilliseconds = 2000;
    private readonly int _maxFailedTries = 5;

    CancellationTokenSource _cts = new CancellationTokenSource();

    public SignalingHub(IChatPersonaRepository chatPersonaRepository)
    {
        _chatPersonaRepository = chatPersonaRepository;
    }

    public override async Task OnConnectedAsync()
    {
        string idUsuarioSesion = ObtenerIdUsuario().ToString();
        Console.WriteLine("new connection");
        //await Groups.AddToGroupAsync(Context.ConnectionId, idUsuarioSesion);
        //_connectionIds.AddOrUpdate(idUsuarioSesion, Context.ConnectionId,
        //        (key, oldValue) => Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    // public override async Task OnDisconnectedAsync(Exception exception)
    // {
    //     string idUsuarioSesion = ObtenerIdUsuario();
    //     await Groups.RemoveFromGroupAsync(Context.ConnectionId, idUsuarioSesion);
    //     await base.OnDisconnectedAsync(exception);
    // }

    private string[] ObtenerUsuariosEnLlamada(string call_id)
    {
        return _chatPersonaRepository.ConsultarPersonasPorChat(int.Parse(call_id))
                                .Select(cp => cp.IdPersona.ToString())
                                .ToArray();
    }
    public string CerrarLlamada(string key, string old_id, string new_id)
    {
        string localUserId = ObtenerIdUsuario();

        // Si es un nuevo id, y el viejo id aun esta conectado a nostros
        if (old_id != new_id && _peerIds.TryGetValue(old_id, out string value) && value == localUserId)
        {
            // Limpiar el viejo id y las colas de mensajes
            _peerIds.TryRemove(old_id, out string _);
            _messageQueues[localUserId] = new Queue<Message>();
            _messageQueues[old_id] = new Queue<Message>();
            // Enviar ultimo mensaje para cerrar llamada
            RemoveRemote(old_id);
        };

        return new_id;

    }

    public async Task CrearLlamada(string? callId)
    {
        string localUserId = ObtenerIdUsuario();
        string value;

        Console.WriteLine($"CreateCall - localUserId: {localUserId}");
        _connectionIds.TryGetValue(localUserId, out value);
        if(value != null && value != Context.ConnectionId)
        {
            await RemoveRemote(localUserId);
        }
        _connectionIds[localUserId] = Context.ConnectionId;

        string[] remoteUserIds = ObtenerUsuariosEnLlamada(callId)
            .Where(id => id != localUserId)
            .ToArray();
        Console.WriteLine($"CreateCall - remoteUserIds: {string.Join(',', remoteUserIds)}");

        if (remoteUserIds.Length == 1)
        {
            string remoteUserId = remoteUserIds[0];


            _peerIds.AddOrUpdate(localUserId, remoteUserId,
                (key, oldValue) => CerrarLlamada(key, oldValue, remoteUserId));

            _messageQueues[localUserId] = new Queue<Message>();

            if (IsPeerAlreadyConnected(localUserId, remoteUserId))
            {
                Console.WriteLine($"CreateCall - peer already connected");
                _messageQueues[remoteUserId] = new Queue<Message>();
                SendMessageToPeer("{\"type\": \"callee-connected\"}", remoteUserId);
                return;
            }
        }

        Console.WriteLine($"CreateCall - peer not connected");
        // Send local ID to the caller, to share with the callee
        //Clients.Group(localUserId).LocalId(localUserId);
        _connectionIds.TryGetValue(localUserId, out value);
        Clients.Client(value).LocalId(localUserId);
    }

    private bool IsPeerAlreadyConnected(string localUserId, string remoteUserId)
    {
        return _peerIds.TryGetValue(remoteUserId, out string value) && value == localUserId;
    }

    

    public async Task RemoveRemote(string old_id)
    {
        var message = $"{{\"type\": \"remove-remote\"}}";
        _messageQueues[old_id] = new Queue<Message>();
        await SendMessageToPeer(message, old_id);
      
    }
    public async Task SendMessageToPeer(string message, string peerId = "")
    {
        // Console.WriteLine($"Send Message: {message}");

        if (peerId == "" && _peerIds.TryGetValue(ObtenerIdUsuario(), out string value))
        {
            peerId = value;
        }

        var messageObj = new Message { Id = Guid.NewGuid().ToString(), Content = message };
        _messageQueues[peerId].Enqueue(messageObj);

        if (_messageQueues[peerId].Count == 1){ // Start the timer if this is the first message
            await TrySendNextMessage(peerId); 
        }
    }

    private async Task TrySendNextMessage(string peerId)
    {
        if (_messageQueues.TryGetValue(peerId, out var queue) && queue.Any())
        {
            var message = queue.Peek();
            Console.WriteLine($"Sending message: {message.Id}, Count: {queue.Count}, PeerId: {peerId}");
            Console.WriteLine(message.Content.Substring(0, Math.Min(message.Content.Length, 50)));
            // await Clients.Client(peerId).NewMessage(message);
            //await Clients.Group(peerId).NewMessage(message);
            _connectionIds.TryGetValue(peerId, out string value);
            Clients.Client(value).NewMessage(message);
            StartAckTimeoutTimer(peerId, message);
        }
    }


    public async Task AcknowledgeMessage(string messageId)
    {
        string localId = ObtenerIdUsuario();
        Console.WriteLine($"LocalId: {localId}, Acknowledging: {messageId}");
        if (_messageQueues.TryGetValue(localId, out var queue) && queue.Any())
        {
            var message = queue.Peek();
            Console.WriteLine($"Message: {message.Id}, Count: {queue.Count}");
            if (message.Id == messageId)
            {
                queue.Dequeue(); // Remove the message that has been acknowledged
            }
        }
    }

    private void StartAckTimeoutTimer(string peerId, Message message)
    {
        var timer = new System.Timers.Timer(_ackTimeoutMilliseconds);
        timer.Elapsed += async (sender, e) => await HandleAckTimeout(sender, peerId, message.Id);
        timer.AutoReset = false;
        timer.Start();
    }

    private async Task HandleAckTimeout(object sender, string peerId, string messageId)
    {
        // Stop and dispose the timer
        var timer = sender as System.Timers.Timer;
        timer?.Stop();
        timer?.Dispose();

        // Check if the message has been acknowledged
        if (_messageQueues.TryGetValue(peerId, out var queue) && queue.Any())
        {
            var message = queue.Peek();
            if (message.Id == messageId)
            {
                message.Tries++;
                Console.WriteLine($"Failed Tries: {message.Tries}");
                if(message.Tries >= _maxFailedTries && queue.Any()){
                    queue.Dequeue();
                }
            }

            await TrySendNextMessage(peerId);

        }
    }


    private string ObtenerConnectionId()
    {
        if (Context.ConnectionId is null)
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        if (Context.ConnectionId.Length <= 0)
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        return Context.ConnectionId;
    }

    private string ObtenerIdUsuario()
    {
        if (Context.UserIdentifier is null)
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        if (!int.TryParse(Context.UserIdentifier, out int idUsuario))
        {
            throw new Exception("No se pudo obtener el id del usuario");
        }

        return $"{idUsuario}";
    }

}