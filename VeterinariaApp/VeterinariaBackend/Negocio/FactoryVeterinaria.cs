using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinariaBackend.Acceso_a_Datos.Implementaciones;
using VeterinariaBackend.Acceso_a_Datos.Interfaces;

namespace VeterinariaBackend.Negocio
{
    public class FactoryVeterinaria : AbstractFactory
    {
        public override IGestorVeterinaria CrearGestor()
        {
            return new GestorVeterinaria();
        }
    }
}
