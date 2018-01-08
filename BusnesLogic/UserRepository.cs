using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnesLogic
{
    public abstract class UserRepository
    {
        public abstract IEnumerable<User> GetUsers();
        public abstract User GetUser(int id);
        public abstract void Add(User user);
        public abstract void Update(User user);
    }
}
