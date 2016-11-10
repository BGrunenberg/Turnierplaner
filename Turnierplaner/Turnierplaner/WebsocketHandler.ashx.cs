using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace WebSocketServer
{
    /// <summary>
    /// Summary description for WebSocketRequestHandler1
    /// </summary>
    public class WebSocketRequestHandler1 : IHttpHandler
    {
        private static List<WebSocket> clients = new List<WebSocket>();


        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
                context.AcceptWebSocketRequest(Answer);
        }

        private async Task Answer(AspNetWebSocketContext con)
        {
            WebSocket socket = con.WebSocket;
            clients.Add(socket);

            while (socket.State == WebSocketState.Open)
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                foreach (WebSocket client in clients)
                {
                    if (!client.Equals(socket))
                        await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }

            clients.Remove(socket);
        }
    }
}