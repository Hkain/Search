using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnesLogic
{
    public class UserService
    {
        private readonly UserRepository _repository;
        public UserService(UserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException("User service BLL repository = null");
        }

        public IEnumerable<User> GetUsers() => _repository.GetUsers();

        public User GetUser(int id) => _repository.GetUser(id);

        public void Add(User user) => _repository.Add(user);

        public void Update(User user) => _repository.Update(user);
    }
}
