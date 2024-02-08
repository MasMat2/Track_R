using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Collections.Concurrent;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SignalingHub : Hub<ISignalingHub>
{

    private static readonly ConcurrentDictionary<string, Queue<Message>> _messageQueues = new ConcurrentDictionary<string, Queue<Message>>();

    private static readonly ConcurrentDictionary<string, string> peer_ids = new ConcurrentDictionary<string, string>();
    private readonly int _ackTimeoutMilliseconds = 500;

    CancellationTokenSource _cts = new CancellationTokenSource();

    public SignalingHub(){
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task CrearLlamada(string? caller_id)
    {       
        string local_id = ObtenerIdUsuario();
        Console.WriteLine($"{local_id}, {caller_id}");

        // Exchange id with peer
        if(caller_id != null){
            
            peer_ids.GetOrAdd(local_id, caller_id);
            peer_ids.GetOrAdd(caller_id, local_id);

            SendMessageToPeer($"{{\"type\": \"callee-connected\"}}");
        }
        else{
            // Send local id to the caller, to share with the callee
            Clients.Client(local_id).LocalId(local_id);
        }

    }
    

public async Task SendMessageToPeer(string message)
{
    Console.WriteLine($"Send Message: {message}");
    string peerId = peer_ids.GetOrAdd(ObtenerIdUsuario(), "");
    if (!_messageQueues.ContainsKey(peerId))
    {
        _messageQueues[peerId] = new Queue<Message>();
    }

    var messageObj = new Message { Id = Guid.NewGuid().ToString(), Content = message };
    _messageQueues[peerId].Enqueue(messageObj);

    if(_messageQueues[peerId].Count == 1){ // Start the timer if this is the first message
        await TrySendNextMessage(peerId); 
    }
}

    private async Task TrySendNextMessage(string peerId)
    {
        if (_messageQueues.TryGetValue(peerId, out var queue) && queue.Any())
        {
            var message = queue.Peek();
            Console.WriteLine($"Sending message: {message.Id}, Count: {queue.Count}");
            await Clients.Client(peerId).NewMessage(message);
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
        await TrySendNextMessage(localId); // Try to send the next message
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
                await TrySendNextMessage(peerId);
            }
        }
    }


    private string ObtenerIdUsuario()
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

}

