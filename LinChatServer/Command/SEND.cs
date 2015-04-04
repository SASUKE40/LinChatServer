using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinChatServer.Command
{
    using global::LinChatServer.Model;

    using SuperWebSocket;
    using SuperWebSocket.SubProtocol;

    public class SEND : JsonSubCommand<ChatSession, Chat>
    {
        protected override void ExecuteJsonCommand(ChatSession session, Chat commandInfo)
        {
            Console.WriteLine(@"{0:HH:mm:ss}  接受客户端:{1}的消息：{2}", DateTime.Now, session.SessionID, commandInfo.Content);
            var msg = string.Format("{0:HH:mm:ss} {1}说: {2}", DateTime.Now, commandInfo.Sender, commandInfo.Content);
            this.SendToAll(session, msg);
        }


        public void SendToAll(ChatSession session, string msg)
        {
            foreach (var webSocketSession in session.AppServer.GetAllSessions())
            {
                webSocketSession.Send(msg);
            }
        }
    }
}
