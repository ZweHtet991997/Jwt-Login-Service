using Login_Service_with_JWT_Auth.Auth;
using Login_Service_with_JWT_Auth.Models;
using Login_Service_with_JWT_Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Login_Service_with_JWT_Auth.Controllers
{
    public class LoginController : Controller
    {
        private LoginServices _loginServices;
        private JWTAuth _JWTAuth;

        public LoginController(LoginServices loginServices, JWTAuth jWTAuth)
        {
            _loginServices = loginServices;
            _JWTAuth = jWTAuth;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            var dataResult = await _loginServices.Register(model);
            return dataResult > 0 ? Ok("Success") : BadRequest();
        }

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var dataResult = await _loginServices.Login(model);
            if (dataResult > 0)
            {
                //get token after login credentials is valid
                var token = await _JWTAuth.GetJWTToken(model);
                if (!string.IsNullOrEmpty(token))
                {
                    LoginResponseModel responseModel = new LoginResponseModel();
                    responseModel.Id = dataResult;
                    responseModel.Token = token;
                    return Content(JsonConvert.SerializeObject(responseModel), "application/json");
                }
            }
            return dataResult > 0 ? Ok(dataResult) : Unauthorized("User Name or Password is incorrect");
        }

        [Authorize]
        //use  authorization attribute in the action or controller that you want to authorize access
        //get user profile is under authorize access
        [HttpPost]
        [Route("api/userprofile")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var dataResult = await _loginServices.GetUserProfile(userId);
            return dataResult != null ? Ok(dataResult) : NotFound();
        }
    }
}
