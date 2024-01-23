using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TrackrAPI.Hubs;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SignalingHub : Hub<ISignalingHub>
{

    public SignalingHub(){

    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task CrearLlamada(string? peer_id)
    {       
        string local_id = this.ObtenerIdUsuario();
        // Exchange id with peer
        if(peer_id != null){
            Console.WriteLine($"{local_id}, {peer_id}");
            await Clients.Caller.PeerId(peer_id); // send it to the peer
            await Clients.Client(peer_id).PeerId(local_id); // send it to the peer
        }
        else{
            // Send local id to the calle, to share with the caller
            Clients.Client(local_id).LocalId(local_id);
        }
    
    }
    
    public async Task EnviarMensaje(string peer_id, string json_string)
    {
        Console.WriteLine($"Peer: {peer_id} Mensaje: {json_string}");
        await Clients.Client(peer_id).NewMessage(json_string);
    }

    private string ObtenerIdUsuario()
    {
        Console.WriteLine(Context.ConnectionId);
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