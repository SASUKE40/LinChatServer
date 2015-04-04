using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinChatServer
{
    using System.Data.Entity;

    using LinChatServer.Model;

    class SqliteContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
