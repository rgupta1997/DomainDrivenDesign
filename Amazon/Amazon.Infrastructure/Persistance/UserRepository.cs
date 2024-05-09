using Amazon.Application.Commnon.Interfaces.Persistance;
using Amazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();
        public void AddUser(User user)
        {
           _users.Add(user);
        }

        public User? GetUserbyEmail(string email)
        {
            return _users.SingleOrDefault(user => user.Email == email);
        }
    }
}
