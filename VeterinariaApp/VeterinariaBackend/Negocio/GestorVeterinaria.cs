using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinariaBackend.Acceso_a_Datos.Implementaciones;
using VeterinariaBackend.Acceso_a_Datos.Interfaces;
using VeterinariaBackend.Dominio;

namespace VeterinariaBackend.Negocio
{
     public class GestorVeterinaria : IGestorVeterinaria
    {
        private IMascotaDao _mascotaDao;

        public GestorVeterinaria()
        {
            _mascotaDao = new MascotaDao();
        }
        //public GestorVeterinaria(AbstractFactory factory)
        //{
        //    _mascotaDao = factory.CrearMascotaDao();
        //}

        // MASCOTA
        public int GetIdMascota(int cliente, string nombre)
        {
            return _mascotaDao.GetIdMascota(cliente, nombre);
        }
        public List<Mascota> ObtenerMascotaCliente(int cod)
        {
            return _mascotaDao.ConsultaMascotaCliente(cod);
        }
        public bool AgregarMascotaAtencion(Mascota mascota, int id)
        {
            return _mascotaDao.AgregarMascotaAtencion(mascota, id);
        }
        public bool DeleteMascota(int idMascota)
        {
            return _mascotaDao.DeleteMascota(idMascota);
        }
        public bool UpdateMascota(Mascota mascota)
        {
            return _mascotaDao.UpdateMascota(mascota);
        }


        //ATENCION
        public List<Atencion> ObtenerAtencion(int cod)
        {
            return _mascotaDao.ObtenerAtencion(cod);
        }
        public List<int> GetIdAtencion(int idMascota)
        {
            return _mascotaDao.GetIdAtencion(idMascota);
        }
        public int ProximoDetalle(int idMascota)
        {
            return _mascotaDao.ProximoDetalle(idMascota);
        }
        public bool InsertarAtencion(Mascota oMascota)
        {
            return _mascotaDao.InsertarAtencion(oMascota);
        }
        public bool InsertarDetalleAtencion(List<Atencion> atencion, int id)
        {
            return _mascotaDao.InsertarDetalleAtencion(atencion, id);
        }
        public bool UpdateAtencion(Atencion atencion, int id)
        {
            return _mascotaDao.UpdateAtencion(atencion, id);
        }
        public bool DeleteAtencion(int idMascota)
        {
            return _mascotaDao.DeleteAtencion(idMascota);
        }
        public bool DeleteDetalleAtencion(int idMascota, int idDetalle)
        {
            return _mascotaDao.DeleteDetalleAtencion(idMascota, idDetalle);
        }

        //CLIENTE

        public bool InsertarCliente(Clientes cliente)
        {
            return _mascotaDao.InsertarCliente(cliente);
        }
        public bool UpdateCliente(Clientes cliente)
        {
            return _mascotaDao.UpdateCliente(cliente);
        }

        public bool DeleteCliente(int id)
        {
            return _mascotaDao.DeleteCliente(id);
        }

        //LOGIN

        public bool LoginCheck(string user, string pass)
        {
            return _mascotaDao.LoginCheck(user, pass);
        }



        public bool InsertarMascota(Mascota oMascota, int cod)
        {
            return _mascotaDao.InsertarMascota(oMascota, cod);
        }

        

        public List<Clientes> ObtenerClientes()
        {
            return _mascotaDao.ConsultarClientes();
        }

        
        //public DataTable MascotaNombre(string nombre)
        //{
        //    return _mascotaDao.ConsultarMascotaNombre(nombre);
        //}
       
        

       
        
        
     
       
      

        

       

       
        
        
        //public bool InsertarDetalleAtencion(List<Atencion> atencion)
        //{
        //    return _mascotaDao.InsertarDetalleAtencion(atencion);
        //}
    }
}
