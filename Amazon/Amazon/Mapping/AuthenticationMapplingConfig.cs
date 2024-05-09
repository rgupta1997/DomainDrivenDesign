using Amazon.Application.Authentication.Common;
using Amazon.Contract.Authentication;
using Mapster;

namespace Amazon.Mapping
{
    public class AuthenticationMapplingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                //.Map(dest => dest.guid, src => src.User.Id)
                .Map(dest => dest, src => src.User);
        }
    }
}
