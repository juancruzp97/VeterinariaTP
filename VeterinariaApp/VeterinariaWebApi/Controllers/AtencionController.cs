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
    public class AtencionController : ControllerBase
    {

        private IGestorVeterinaria servicio;

        public AtencionController()
        {
            servicio = new GestorVeterinaria();
        }



        [HttpGet("GetAtencion/{id}")]
        public ActionResult GetAtencion(int id)
        {
            if (servicio.ObtenerAtencion(id).Count == 0)
            {
                return BadRequest("Problemas al consultar Mascota");
            }
            else
            {
                return Ok(servicio.ObtenerAtencion(id));
            }
        }

        [HttpGet("GetDetalleAtencion/{id}")]
        public ActionResult GetDetalleAtencion(int id)
        {

            if (servicio.GetIdAtencion(id).Count == 0)
            {
                return BadRequest("Problemas al consultar Mascota");
            }
            else
            {
                return Ok(servicio.GetIdAtencion(id));
            }
        }

        [HttpGet("ProximoDetalle/{id}")]
        public IActionResult ProximoDetalle(int id)
        {
            if (servicio.ProximoDetalle(id) < 0)
            {
                return BadRequest("Error al consultar Proximo Detalle");
            }
            else
            {
                return Ok(servicio.ProximoDetalle(id));
            }
        }

        // POST api/<AtencionController>
        [HttpPost("InsertarAtencion")]
        public IActionResult InsertarAtencion(Mascota mascota)
        {
            if (servicio.InsertarAtencion(mascota) == false)
            {
                return BadRequest("Fallo insertar Atencion");
            }
            else
            {
                return Ok("Atencion agregada con Exito");
            }
        }

        [HttpPost ("InsertarDetalleAtencion/{id}")]
        public IActionResult InsertarDetalleAtencion(List<Atencion> atencion, int id)
        {
            if(servicio.InsertarDetalleAtencion(atencion, id) == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        // PUT api/<AtencionController>/5
        [HttpPut("UpdateDetalleAtencion/{id}")]
        public IActionResult Put(Atencion atencion, int id)
        {
            if(atencion == null)
            {
                return BadRequest();
            }
            if (servicio.UpdateAtencion(atencion, id))
            {
                return Ok("Atencion Actualizada");
            }
            else
            {
                return BadRequest("No se pudo Actualizar");
            }
        }


        // DELETE

        [HttpDelete("DeleteAtencion/{id}")]
        public IActionResult DeleteAtencion(int id)
        {
            if (servicio.DeleteAtencion(id) == false)
            {
                return BadRequest("Problemas al eliminar Atencion");
            }
            else
            {
                return Ok("Ok");
            }
        }

        [HttpDelete("DeleteDetalle/{id}/{det}")]
        public IActionResult DeleteDetalle(int id, int det)
        {
            if (servicio.DeleteDetalleAtencion(id, det) == false)
            {
                return BadRequest("Problemas al eliminar Detalle Atencion");
            }
            else
            {
                return Ok("Detalle Borrado");
            }
        }
    }
}
