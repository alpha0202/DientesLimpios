using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{


    [TestClass]
    public class EmailTests
    {

        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_EmailNulo_LanzarExcepcion()
        {
            new Email(null!);

        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_EmailSinArroba_LanzarExcepcion()
        {
            new Email("edwin.com");

        }
        [TestMethod]
        public void Construir_EmailValido_NoLanzaExcepcion()
        {
            new Email("alpha0202@gmail.com");

        }
    }

}
