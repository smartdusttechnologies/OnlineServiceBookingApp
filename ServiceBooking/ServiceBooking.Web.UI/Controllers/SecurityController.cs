using Microsoft.AspNetCore.Mvc;
using ServiceBooking.Web.Models;
using ServiceBooking.DTO;
using ServiceBooking.Business.Common;
using AutoMapper;
using ServiceBooking.Buisness.Core.Model.Security;
using ServiceBooking.Buisness.Core.Model;
using MediatR;
using ServiceBooking.Buisness.Features.Authentication.Commands;
using ServiceBooking.Buisness.Features.SecurityParamters.Queries;
using ServiceBooking.Buisness.Features.User.Queries;

namespace ServiceBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public SecurityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile(int userId)
        {
            var result = _mediator.Send(new Buisness.Features.User.Queries.Profile.Command(userId)).Result;
            return Ok(result);
        }
        [HttpGet]
        [Route("ForgotPassword")]
        public IActionResult ForgetPassword(string email)
        {
            var result = _mediator.Send(new Buisness.Features.User.Queries.Profile.Command(4)).Result;
            return Ok(result);
        }
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp(UserDTO user)
        {
            var userModel = _mapper.Map<UserDTO,UserModel>(user);
            userModel.IsActive = true;
            var result = _mediator.Send(new SignUp.Command(userModel)).Result;
            if (result.IsSuccessful)
            {
                List<ValidationMessage> success = new List<ValidationMessage>
                {
                    new ValidationMessage { Reason = "Sign Up Successfully", Severity = ValidationSeverity.Information, SourceId = "fields" }
                };
                result.Message = success;
                return Json(result);
            }
            return BadRequest("sf");
        }
        /// <summary>
        /// Method to get the Login details from UI and Process Login.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginDTO loginRequest)
        {
            var loginReq = new LoginRequest { UserName = loginRequest.UserName, Password = loginRequest.Password };
            RequestResult<LoginToken> result = _mediator.Send(new Login.Command(loginReq)).Result;
            if (result.IsSuccessful)
            {
                List<ValidationMessage> success = new List<ValidationMessage>
                {
                    new ValidationMessage { Reason = "Login Successfully", Severity = ValidationSeverity.Information, SourceId = "fields" }
                };
                result.Message = success;
                return Json(result);
            }
            return BadRequest(result);
        }
        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDTO changepasswordDTO)
        {
            if (ModelState.IsValid)
            {
                var changepasswordRequest = new ChangePasswordModel()
                {
                    OldPassword = changepasswordDTO.OldPassword,
                    NewPassword = changepasswordDTO.NewPassword,
                    ConfirmPassword = changepasswordDTO.ConfirmPassword,
                    Username = changepasswordDTO.Username,
                    UserId = changepasswordDTO.UserId,
                };
                var result = _mediator.Send(new ChangePassword.Command(changepasswordRequest)).Result;
                if (result.IsSuccessful)
                {
                    List<ValidationMessage> success = new List<ValidationMessage>
                {
                    new ValidationMessage { Reason = "Password Changed Successfully", Severity = ValidationSeverity.Information, SourceId = "fields" }
                };
                    result.Message = success;
                    return Ok(result);
                }
                return BadRequest(result);
            }
            else
            {
                List<ValidationMessage> errors = new List<ValidationMessage>
                {
                    new ValidationMessage { Reason = "All Fields Are Required", Severity = ValidationSeverity.Error, SourceId = "fields" }
                };
                return Json(new RequestResult<bool>(errors));
            }
        }
        [HttpGet]
        [Route("EmailExist")]
        public IActionResult EmailExist(string email)
        {
            var result = _mediator.Send(new EmailExist.Command(email)).Result;
            if(result.IsSuccessful)
            {
                return Ok(true);
            }
            return BadRequest(result.Message);
        }
    }
}
