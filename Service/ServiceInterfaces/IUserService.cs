using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IUserService
    {
        public void AddUser(User user);

        public void UpdateUser(User user);

        public User GetUser(int id);

        public void DeleteUser(int id);

        public IEnumerable<User> GetUsers();

    }
}
