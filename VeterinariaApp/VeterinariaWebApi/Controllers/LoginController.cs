using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaBackend.Negocio;

namespace VeterinariaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IGestorVeterinaria servicio;
        public LoginController()
        {
            servicio = new GestorVeterinaria();
        }

        [HttpGet("LoginCheck/{user}/{pass}")]
        public IActionResult LoginCheck(string user, string pass)
        {
            if (servicio.LoginCheck(user, pass))
            {
                return Ok("Login Correcto!");
            }
            else
            {
                return BadRequest("Error!");
            }
        }

    }
}
