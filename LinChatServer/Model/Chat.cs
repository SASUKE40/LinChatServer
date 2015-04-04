using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinChatServer.Model
{
    public class Chat
    {
        public Int64 Id { get; set; }
        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return string.Format("Sender: {0}, Receiver: {1}, Content: {2}", this.Sender, this.Receiver, this.Content);
        }
    }
}
