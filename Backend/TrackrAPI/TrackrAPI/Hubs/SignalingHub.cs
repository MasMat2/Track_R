using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Collections.Concurrent;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SignalingHub : Hub<ISignalingHub>
{

    private static readonly ConcurrentDictionary<string, Queue<Message>> _messageQueues = new ConcurrentDictionary<string, Queue<Message>>();

    private static readonly ConcurrentDictionary<string, string> _peer_ids = new ConcurrentDictionary<string, string>();
    private readonly int _ackTimeoutMilliseconds = 2000;
    private readonly int _maxFailedTries = 5;

    CancellationTokenSource _cts = new CancellationTokenSource();

    public SignalingHub(){
    }

    public override async Task OnConnectedAsync()
    {
        string idUsuarioSesion = ObtenerIdUsuario().ToString();
        await Groups.AddToGroupAsync(Context.ConnectionId, idUsuarioSesion);
        await base.OnConnectedAsync();
    }

    // public override async Task OnDisconnectedAsync(Exception exception)
    // {
    //     string idUsuarioSesion = ObtenerIdUsuario();
    //     await Groups.RemoveFromGroupAsync(Context.ConnectionId, idUsuarioSesion);
    //     await base.OnDisconnectedAsync(exception);
    // }

    public async Task CrearLlamada(string? caller_id)
    {       
        string local_id = ObtenerIdUsuario();
        Console.WriteLine($"{local_id}, {caller_id}");

        // Exchange id with peer
        if(caller_id != null){
            
            // Set connectins and clear queues
            _peer_ids.AddOrUpdate(local_id, caller_id, (key, oldValue) => RemoveRemote(key, oldValue, caller_id));
            _messageQueues[local_id] = new Queue<Message>();

            _peer_ids.AddOrUpdate(caller_id, local_id, (key, oldValue) => RemoveRemote(key, oldValue, local_id));
            _messageQueues[caller_id] = new Queue<Message>();

            SendMessageToPeer($"{{\"type\": \"callee-connected\"}}");
        }
        else{
            // Send local id to the caller, to share with the callee
            // Clients.Client(local_id).LocalId(local_id);
            Clients.Group(local_id).LocalId(local_id);
        }

    }
    



    public string RemoveRemote(string key, string old_id, string new_id)
    {
        Console.WriteLine(key, old_id, new_id);
        if (old_id != new_id)
        {
            var message = $"{{\"type\": \"remove-remote\"}}";
            _messageQueues[old_id] = new Queue<Message>();
            SendMessageToPeer(message, old_id);
        }
        return new_id;
    }
    public async Task SendMessageToPeer(string message, string peerId = "")
    {
        // Console.WriteLine($"Send Message: {message}");
        peerId = peerId == "" ? _peer_ids.GetOrAdd(ObtenerIdUsuario(), ""): peerId;

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
            // await Clients.Client(peerId).NewMessage(message);
            await Clients.Group(peerId).NewMessage(message);
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