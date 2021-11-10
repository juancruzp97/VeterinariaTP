using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinariaBackend.Dominio
{
    enum Tipo
    {
        perro = 1,
        gato = 2,
        arania = 3,
        iguana = 4
    }
    public class Mascota
    {

            public int CodigoMascota { get; set; }
            public string Nombre { get; set; }
            public int Edad { get; set; }
            public int TipoMascota { get; set; }

            public List<Atencion> ListaAtencion { get; set; }

            public Mascota()
            {
                ListaAtencion = new List<Atencion>();

            
            }
    


            public void AgregarAtencion(Atencion atencion)
            {
                ListaAtencion.Add(atencion);
            }

            public void QuitarAtencion(int indice)
            {
                ListaAtencion.RemoveAt(indice);
            }

        }
    
}
