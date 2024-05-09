using Amazon.Application.Authentication.Commands.Register;
using Amazon.Application.Authentication.Common;
using Amazon.Application.Authentication.Queries;
using Amazon.Contract.Authentication;
using Amazon.Contract.Common;
using Amazon.Validator;
using FluentValidation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {       
        private readonly IMediator  _mediator;
        private readonly IMapper  _mapper;
        private readonly AbstractValidator<ClaimSearch> _validator;
        private readonly AbstractValidator<MedicalDetails>  _medicalvalidator;

        public AuthenticationController(IMediator mediator, IMapper mapper, AbstractValidator<ClaimSearch> validations, AbstractValidator<MedicalDetails> medicalvalidator)  
        {
            _mapper = mapper;   
            _mediator = mediator;
            _validator = validations;
            _medicalvalidator = medicalvalidator;
        }

        [HttpPost("claimsearch")]
        public async Task<ActionResult> ClaimSearch(ClaimSearch request)
        {
            var validate = _validator.Validate(request);

            if (validate.IsValid)
            {
                return Ok(new Result<ClaimSearch>(StatusCodes.Status200OK, null, request));
            }
            return BadRequest(new Result<ClaimSearch>(StatusCodes.Status400BadRequest, validate.Errors.Select(error => error.ErrorMessage).ToList(), null));

        }


        [HttpPost("medicaldetails")]
        public async Task<ActionResult> MedicalDetails(MedicalDetails request)
        {
            var validate = _medicalvalidator.Validate(request);

            if (validate.IsValid)
            {
                return Ok(new Result<MedicalDetails>(StatusCodes.Status200OK, null, request));
            }
            return BadRequest(new Result<MedicalDetails>(StatusCodes.Status400BadRequest, validate.Errors.Select(error => error.ErrorMessage).ToList(), null));

        }



        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest registerRequest)
        {
            var command = _mapper.Map<RegisterCommand>(registerRequest);
            var authresult = await _mediator.Send(command);
            var response = _mapper.Map<AuthenticationResponse>(authresult);
            //new Result<AuthenticationResult>(StatusCodes.Status200OK,response,)
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var query = _mapper.Map<LoginQuery>(loginRequest);  
            var authresult = await _mediator.Send(query);
            var response = _mapper.Map<AuthenticationResponse>(authresult);

            return Ok(response);
        }


    }
}
