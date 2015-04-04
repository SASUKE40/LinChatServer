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

    public class LIST : JsonSubCommand<ChatSession, Chat>
    {
        protected override void ExecuteJsonCommand(ChatSession session, Chat commandInfo)
        {
            Console.WriteLine(@"{0:HH:mm:ss}  接受客户端:{1} {2}的List请求", DateTime.Now, commandInfo.Sender, session.SessionID);
            var msg = string.Format("{0:HH:mm:ss} {1}说: {2}", DateTime.Now, commandInfo.Sender, commandInfo.Content);
            session.Sender = commandInfo.Sender;
            session.IsLogin = true;
            foreach (var webSocketSession in session.AppServer.GetAllSessions())
            {
                if (webSocketSession.IsLogin && webSocketSession.Sender != session.Sender && !string.IsNullOrWhiteSpace(webSocketSession.Sender))
                {
                    Console.WriteLine("From: " +session.Sender + " To: " + webSocketSession.Sender);
                    //Get its list
                    session.Send(webSocketSession.Sender);
                    //Broadcast itselp
                    webSocketSession.Send(session.Sender);
                }
            }
    }
    }
}
