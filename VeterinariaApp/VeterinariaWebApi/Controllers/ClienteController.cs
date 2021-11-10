using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaBackend.Dominio;
using VeterinariaBackend.Negocio;

namespace VeterinariaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

            private IGestorVeterinaria servicio;
            public ClienteController()
           {                servicio = new GestorVeterinaria();          
        
        }

        [HttpPost("InsertarCliente")]
        public IActionResult InsertarCliente(Clientes cliente)
        {
            if(cliente == null)
            {
                return BadRequest();
            }
            if (servicio.InsertarCliente(cliente))
            {
                return Ok("Cliente Agregado con Exito!");
            }
            else
            {
                return BadRequest("Error al guardar Cliente");
            }

        }

        [HttpPut("UpdateCliente")]
        public IActionResult UpdateCliente(Clientes clientes)
        {
            if (clientes == null)
            {
                return BadRequest();
            }else if (servicio.UpdateCliente(clientes))
            {
                return Ok("Cliente Actualizado!");
            }
            else
            {
                return BadRequest("Error al Actualizar Cliente");
            }

        }

        [HttpDelete("EliminarCliente/{id}")]
        public IActionResult DeleteCliente(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Error al eliminar Cliente");
            }
            else if (servicio.DeleteCliente(id))
            {
                return Ok("Cliente Eliminado!");
            }
            else
            {
                return BadRequest("Error!");
            }
        }

    }
}
