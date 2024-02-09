﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Services.Seguridad;

namespace TrackrAPI.Controllers.Seguridad
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate/{esMobile}")]
        public IActionResult Authenticate(LoginRequest loginRequest , bool esMobile)
        {
            try
            {
                var loginResult = loginService.Authenticate(loginRequest , esMobile);
                return Ok(loginResult);
            }
            catch (LoginSinLocacionException ex)
            {
                var error = new {
                    ex.Message,
                    ex.Locaciones
                };

                return BadRequest(error);
            }
        }


    }
}
