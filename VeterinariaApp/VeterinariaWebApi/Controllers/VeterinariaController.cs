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
    public class VeterinariaController : ControllerBase
    {
        private IGestorVeterinaria servicio;
        public VeterinariaController()
        {
            servicio = new GestorVeterinaria();
        }

        //public VeterinariaController()
        //{
        //    AbstractFactory factory = new FactoryVeterinaria();
        //    servicio = factory.CrearGestor();
        //}

        // GET: api/<VeterinariaController>
        [HttpGet("ConsultarCliente")]
        public ActionResult GetCliente()
        {
            if (servicio.ObtenerClientes().Count == 0)
            {
                return BadRequest("Problemas al consultar Cliente");
            }
            else
            {
                return Ok(servicio.ObtenerClientes());
            }


        }

        //[HttpGet("GetIdMascota/{id}/{nombre}")]
        //public ActionResult GetIdMascota(int id, string nombre)
        //{
            
        //    if (servicio.GetIdMascota(id, nombre) == 0)
        //    {
        //        return BadRequest("Problemas al consultar Mascota");
        //    }
        //    else
        //    {
        //        return Ok(servicio.GetIdMascota(id, nombre));
        //    }


        //}
        //[HttpGet("GetAtencion/{id}")]
        //public ActionResult GetAtencion(int id)
        //{

        //    if (servicio.ObtenerAtencion(id).Count == 0)
        //    {
        //        return BadRequest("Problemas al consultar Mascota");
        //    }
        //    else
        //    {
        //        return Ok(servicio.ObtenerAtencion(id));
        //    }


        //}
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

        //[HttpGet("ConsultarMascota/{id}")]
        //public ActionResult GetMascota(int id)
        //{
        //    if (servicio.ObtenerClientes().Count == 0)
        //    {
        //        return BadRequest("Problemas al consultar Cliente");
        //    }
        //    else
        //    {
        //        return Ok(servicio.ObtenerMascotaCliente(id));
        //    }


        //}

        // GET api/<VeterinariaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VeterinariaController>
        [HttpPost("AgregarMascotaAtencion/{id}")]
        public IActionResult PostMascotaAtencion(Mascota oMascota, int id)
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
                return Ok("No se pudo grabar la reseta");
            }
        }
        // POST api/<VeterinariaController>
        //[HttpPost("AgregarMascota/{id}")]
        //public IActionResult PostMascota(Mascota oMascota, int id)
        //{
        //    if (oMascota == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (servicio.InsertarMascota(oMascota, id))
        //    {
        //        return Ok("Ok");
        //    }
        //    else
        //    {
        //        return Ok("No se pudo grabar la Mascota");
        //    }
        //}

        // PUT api/<VeterinariaController>/5

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
           // if()
        }


    }
}
