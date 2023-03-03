using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using MongoDB.Bson;
using Movie_API.Filter;
using Movie_API.Models;
using MovieStore_API.Models;
using MovieStore_API.Models.DTOs;
using MovieStore_API.Models.DTOs.Validator;
using MovieStore_API.Services.Abstract;
using MovieStore_API.Services.Concrete;
using System.Net.Http.Headers;

namespace Movie_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IEmailService _emailService;

        public LoginController(IUserService userService, ITokenGeneratorService tokenGeneratorService, IEmailService emailService)
        {
            _userService = userService;
            _tokenGeneratorService = tokenGeneratorService;
            _emailService = emailService;
        }


        [ValidationFilter]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                var user_Id = ObjectId.GenerateNewId().ToString();
                var newUser = await _userService.AddUser(user, user_Id);
                _emailService.CreateEmailVerificationToken(user_Id);
                return Ok(newUser);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginDTO loginUser)
        {
            try
            {
                var user = _userService.GetUser(loginUser);
                user.Result.Token = _tokenGeneratorService.GenerateToken();

                return Ok(user.Result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveUser(string id)
        {
            var user = _userService.GetUserById(id);
            _userService.DeleteUser(user.Result);
            return Ok();
        }
        [HttpGet]
        [Route("activatemail/id={Id}&VerificationToken={EmailVerificationToken}")]
        public IActionResult ActivateEmail(string Id, string EmailVerificationToken)
        {
            var emailVerification = _emailService.GetEmailVerification(Id);
            if(emailVerification.EmailVerificationToken.AccessToken == EmailVerificationToken)
            {
                var user = _userService.GetUserById(Id);
                user.Result.isActivatedEmail = true;
                _userService.UpdateUser(user.Result);
                return Ok(user.Result);
            }
            return BadRequest();
        }
        [HttpGet("register/{id}")]
        public IActionResult GetEmailVerificationToken(string id)
        {
            var emailVerification = _emailService.GetEmailVerification(id);
            return Ok(emailVerification);
        }
    }
}
