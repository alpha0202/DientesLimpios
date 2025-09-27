using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class ConsultorioTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_NombreNulo_LanzaExcepcion()
        {
            new Consultorio(null!);
        }
    }
}
