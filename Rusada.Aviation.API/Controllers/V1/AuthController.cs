using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rusada.Aviation.API.Extensions;
using Rusada.Aviation.Core.Contracts.Requests;
using Rusada.Aviation.Core.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Rusada.Aviation.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var token = await _authService.LoginAsync(loginModel.Username, loginModel.Password);
            if (token != null)
            {
                var response = ApiResponse.GenerateResponse(true, token, null);

                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout() 
        {
            // This is not implemented for this excercise
            return Ok();
        }
    }
}
