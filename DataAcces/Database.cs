using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces
{
    public class Database : DbContext
    {
        public Database(string conn) : base(conn) { }
        public virtual DbSet<User> Users { get; set; }
    }
}
