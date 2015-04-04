namespace LinChatServer.Command
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using LinChatServer.Model;

    using Newtonsoft.Json;

    using SuperWebSocket.SubProtocol;

    public class LOGIN : JsonSubCommand<ChatSession, User>
    {
        protected override void ExecuteJsonCommand(ChatSession session, User commandInfo)
        {
            Console.WriteLine(
                @"{0:HH:mm:ss}  用户登录 用户名:{1} 密码：{2}",
                DateTime.Now,
                commandInfo.Username,
                commandInfo.Password);
            var result = LoginStatus.UserNonexistence;
            using (var db = new SqliteContext())
            {
                db.Users.Load();
                IList<User> users = db.Users.Local;
                foreach (var user in users)
                {
                    if (user.Username.Equals(commandInfo.Username.Trim()))
                    {
                        if (user.Password.Equals(commandInfo.Password.Trim()))
                        {
                            result = LoginStatus.Success;
                            session.Sender = commandInfo.Username;
                        }
                        else
                        {
                            result = LoginStatus.PasswordError;
                        }
                    }
                }
            }
            var status = new ServerStatus();
            switch (result)
            {
                case LoginStatus.UserNonexistence:
                    status = new ServerStatus { Status = false, Detail = "用户不存在" };
                    session.Send(JsonConvert.SerializeObject(status));
                    break;

                case LoginStatus.PasswordError:
                    status = new ServerStatus { Status = false, Detail = "密码不正确" };
                    session.Send(JsonConvert.SerializeObject(status));
                    break;

                case LoginStatus.Success:
                    status = new ServerStatus { Status = true, Detail = "登录成功" };
                    session.Send(JsonConvert.SerializeObject(status));
                    break;
            }
        }
    }

    public enum LoginStatus
    {
        /// <summary>
        ///     用户不存在
        /// </summary>
        UserNonexistence,

        /// <summary>
        ///     密码错误
        /// </summary>
        PasswordError,

        /// <summary>
        ///     登录成功
        /// </summary>
        Success
    }
}