using DAL.RepoInterfaces;
using Domain;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService : IUserService
    {
        IUserRepository UserRepository;
        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public void AddUser(User user)
        {
            UserRepository.Add(user);
        }

        public void DeleteUser(int id)
        {
            User user = UserRepository.GetSingle(id);
            UserRepository.Delete(user);
        }

        public User GetUser(int id)
        {
            return UserRepository.GetSingle(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return UserRepository.GetAll();
        }

        public void UpdateUser(User user)
        {
            UserRepository.Update(user);
        }
    }
}
