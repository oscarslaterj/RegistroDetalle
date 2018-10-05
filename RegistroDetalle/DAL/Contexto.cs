using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroDetalle.Entidades;
using System.Data.Entity;

namespace RegistroDetalle.DAL
{
    public class Contexto: DbContext
    {
        public DbSet<Personas> Personas { get; set; }

        public Contexto() : base("Constr") { }
    }
}
