using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinariaBackend.Dominio
{
    public class Clientes
    {
        public string Nombre { get; set; }
        public bool Sexo { get; set; }
        public int Codigo { get; set; }
        public int Telefono { get; set; }
        public int Documento { get; set; }
        public string Direccion { get; set; }
        public int Edad { get; set; }

        public List<Mascota> ListMascotas { get; set; }

        public Clientes()
        {
            ListMascotas = new List<Mascota>();
        }

        public void AgregarMascota(Mascota mascota)
        {
            ListMascotas.Add(mascota);
        }
        public void QuitarMascota(int indice)
        {
            ListMascotas.RemoveAt(indice);
        }
    }
}
