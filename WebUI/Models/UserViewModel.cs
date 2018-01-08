using BusnesLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class UserViewModel
    {
        private Lazy<List<User>> _users;
        public List<User> Users
        {
            get { return _users.Value; }
        }

        public void SetUser(IEnumerable<User> user)
        {
            var temp = new Lazy<List<User>>();
            foreach (var u in user)
            {
                temp.Value.Add(u);
            }
            _users = temp;
        }
    }
}