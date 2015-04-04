using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinChatServer.Command
{
    using System.Data.Entity;

    using global::LinChatServer.Model;

    using GalaSoft.MvvmLight.Messaging;

    using Newtonsoft.Json;

    using SuperWebSocket;
    using SuperWebSocket.SubProtocol;

    public class SIGNUP : JsonSubCommand<ChatSession, User>
    {
        protected override void ExecuteJsonCommand(ChatSession session, User commandInfo)
        {
            Console.WriteLine(@"{0:HH:mm:ss}  新用户注册 用户名:{1} 密码：{2}", DateTime.Now, commandInfo.Username, commandInfo.Password);
            int result = 0;
            using (var db = new SqliteContext())
            {
                db.Users.Add(commandInfo);
                result = db.SaveChanges();
            }
            if (result > 0)
            {
                var status = new ServerStatus { Status = true, Detail = "注册成功" };
                session.Send(JsonConvert.SerializeObject(status));
            }
            else
            {
                var status = new ServerStatus { Status = false, Detail = "注册失败" };
                session.Send(JsonConvert.SerializeObject(status));
            }
        }

    }
}
