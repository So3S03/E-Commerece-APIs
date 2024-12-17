﻿using Karim.ECommerce.APIs.Controllers.Controllers._BaseController;
using Karim.ECommerce.Application.Abstraction.Contracts;
using Karim.ECommerce.Shared.Dtos.Security;
using Microsoft.AspNetCore.Mvc;

namespace Karim.ECommerce.APIs.Controllers.Controllers.AuthController
{
    public class AuthenticationController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginUserDto loginData)
        {
            var Result = await serviceManager.AuthServices.LoginAsync(loginData);
            return Ok(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto registerData)
        {
            var Result = await serviceManager.AuthServices.RegisterAsync(registerData);
            return Ok(Result);
        }
    }
}
