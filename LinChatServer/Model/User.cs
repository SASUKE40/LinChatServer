using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinChatServer.Model
{
    public class User
    {
        public Int64 Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("Username: {0}, Password: {1}", this.Username, this.Password);
        }
    }
}
