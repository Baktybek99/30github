using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace _30git.WebSockets
{
    public class WebSockerServer
    {
        public static async Task ConnectAsync()
        {
            using (var client = new ClientWebSocket())
            {
                await client.ConnectAsync(new Uri("ws://localhost:8080/ws/"), CancellationToken.None);
                Console.WriteLine("Подключено к серверу");

                string message = "Привет, сервер!";
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                await client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

                byte[] buffer = new byte[1024];
                var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                string response = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"Ответ от сервера: {response}");

                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Закрытие соединения", CancellationToken.None);
            }
        }
    }
}
