using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RegistroDetalle.Entidades
{
    public class Personas
    {
        [Key]

        public int PersonaID { get; set; }
        public String Nombre { get; set; }
        public String Cedula { get; set; }
        public String Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual List<TelefonosDetalle> Telefonos { get; set }
    
    }
}
