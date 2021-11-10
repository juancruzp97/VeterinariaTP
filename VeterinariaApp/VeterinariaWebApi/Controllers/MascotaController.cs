using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinariaBackend.Dominio;
using VeterinariaBackend.Negocio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VeterinariaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {

        private IGestorVeterinaria servicio;
        public MascotaController()
        {
            servicio = new GestorVeterinaria();
        }



        // GET: api/<MascotaController>



        [HttpGet("GetIdMascota/{id}/{nombre}")]
        public ActionResult GetIdMascota(int id, string nombre)
        {

            if (servicio.GetIdMascota(id, nombre) == 0)
            {
                return BadRequest("Problemas al consultar Mascota");
            }
            else
            {
                return Ok(servicio.GetIdMascota(id, nombre));
            }


        }

        [HttpGet("ConsultarMascota/{id}")]
        public ActionResult GetMascota(int id)
        {
            if (servicio.ObtenerMascotaCliente(id).Count < 0)
            {
                return BadRequest("Problemas al consultar Cliente");
            }
            else
            {
                return Ok(servicio.ObtenerMascotaCliente(id));
            }

        }

        [HttpGet("ObtenerMascota/{id}")]
        public ActionResult ObtenerMascota(int id)
        {
            if (servicio.ObtenerMascotaCliente(id).Count < 0)
            {
                return BadRequest("Problemas al consultar Cliente");
            }
            else
            {
                return Ok(servicio.ObtenerMascotaCliente(id));
            }

        }




        [HttpPost("AgregarMascota/{id}")]
        public IActionResult PostMascota(Mascota oMascota, int id)
        {
            if (oMascota == null)
            {
                return BadRequest();
            }
            if (servicio.AgregarMascotaAtencion(oMascota, id))
            {
                return Ok("Ok");
            }
            else
            {
                return BadRequest("No se pudo grabar la Mascota");
            }
        }


        // DELETE
        [HttpDelete("DeleteMascota/{id}")]
        public IActionResult DeleteMascota(int id)
        {
            if (servicio.DeleteMascota(id) == false)
            {
                return BadRequest("Problemas al eliminar Mascota");
            }
            else
            {
                return Ok("Mascota Eliminada");
            }
        }


        // PUT api/<MascotaController>/5
        [HttpPut("UpdateMascota")]
        public IActionResult PutMascota(Mascota oMascota)
        {
            if(oMascota == null)
            {
                return BadRequest("Factura null");
            }
            if (servicio.UpdateMascota(oMascota))
            {
                return Ok("Mascota Actualizada Correctamente");
            }
            else
            {
                return BadRequest("No se pudo actualizar Mascota");
            }
                
        }

     
    }
}
