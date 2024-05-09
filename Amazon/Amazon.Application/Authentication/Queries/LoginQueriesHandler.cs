using Amazon.Application.Authentication.Commands.Register;
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

namespace Amazon.Application.Authentication.Queries
{
    public class LoginQueriesHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueriesHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            
            if (_userRepository.GetUserbyEmail(request.Email) is not User user)
            {
                throw new Exception("User Doesn't Exist");
            }

            if (user.Password != request.Password)
            {
                throw new Exception("Failed to login. Password is incorrect");
            }

            Guid user_id = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(user_id, request.Email, request.Password);
            return new AuthenticationResult(
                                user,
                                token);
            
        }
    }
}
