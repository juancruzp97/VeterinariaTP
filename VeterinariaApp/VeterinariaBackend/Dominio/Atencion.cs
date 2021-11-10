using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinariaBackend.Dominio
{
    public class Atencion
    {
        public int CodAtencion { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public double Importe { get; set; }

        public Atencion()
        {

        }
    }
}
