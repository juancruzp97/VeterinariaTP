using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinariaBackend.Acceso_a_Datos.Interfaces;

namespace VeterinariaBackend.Negocio
{
  public abstract class AbstractFactory
    {
        public abstract IGestorVeterinaria CrearGestor();
    }
}
