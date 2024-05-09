using Amazon.Application.Authentication.Common;
using Amazon.Application.Commnon.Interfaces.Authentication;
using Amazon.Application.Commnon.Interfaces.Persistance;
using Amazon.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {

            if (_userRepository.GetUserbyEmail(command.Email) is not null)
            {
                throw new Exception("User Already Exist");
            }

            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.AddUser(user);

            Guid user_id = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(user_id, command.FirstName, command.LastName);
            return new AuthenticationResult(user, token);           
            
        }
    }
}
