using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroDetalle.BLL;
using RegistroDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroDetalle.BLL.Tests
{
    [TestClass()]
    public class PersonasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            bool paso;
            Personas personas = new Personas();
            personas.PersonaID = 0;
            personas.Nombre = "Oscar Jimenez";
            personas.Cedula = "056-0001510-0";
            personas.Direccion = "Urb Almanzar";
            personas.FechaNacimiento = DateTime.Now;
            paso = PersonasBLL.Guardar(personas);
            Assert.Fail();
        }

       
    }
}