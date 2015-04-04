namespace LinChatServer
{
    using System;

    using SuperSocket.SocketBase;

    using SuperWebSocket;


    public class ChatSession : WebSocketSession<ChatSession>
    {
        private string sender = string.Empty;

        public string Sender
        {
            get
            {
                return this.sender;
            }
            set
            {
                this.sender = value;
            }
        }

        public bool IsLogin { get; set; }
    }
    public class ChatServer : WebSocketServer<ChatSession>
    {
        //有新会话握手并连接成功
        protected override void OnNewSessionConnected(ChatSession session)
        {
            Console.WriteLine("{0:HH:mm:ss}  与客户端:{1}创建新会话", DateTime.Now, session.SessionID);
            var msg = string.Format("{0:HH:mm:ss} {1} 进入聊天室", DateTime.Now, session.SessionID);
            //            session.Send(msg);
//            this.SendToAll(session, msg);
        }

        //有会话被关闭 可能是服务端关闭 也可能是客户端关闭
        protected override void OnSessionClosed(ChatSession session, CloseReason reason)
        {
            Console.WriteLine("{0:HH:mm:ss}  与客户端:{1}的会话被关闭 原因：{2}", DateTime.Now, session.SessionID, reason);
            var msg = string.Format("{0:HH:mm:ss} {1} 离开聊天室", DateTime.Now, session.SessionID);
//            this.SendToAll(session, msg);
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
