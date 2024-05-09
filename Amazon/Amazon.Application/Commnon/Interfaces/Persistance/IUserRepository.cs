using Amazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Commnon.Interfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserbyEmail(string email);

        void AddUser(User user);
    }
}
