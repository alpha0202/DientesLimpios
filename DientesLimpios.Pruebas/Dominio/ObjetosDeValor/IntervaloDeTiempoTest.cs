using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.ObjetosDeValor
{
    [TestClass]
    public class IntervaloDeTiempoTest
    {
        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_FechaInicioPosteriorAFechaFin_LanzaExcepcion()
        {
            var inicio = DateTime.UtcNow;
            var fin = DateTime.UtcNow.AddDays(-1);
            new DientesLimpios.Dominio.ObjetosDeValor.IntervaloDeTiempo(inicio, fin);
        }

        [TestMethod]
        
        public void Construir_ParametrosCorrectos_NoLanzaExcepcion()
        {
            var inicio = DateTime.UtcNow;
            var fin = DateTime.UtcNow.AddMinutes(30);
            new DientesLimpios.Dominio.ObjetosDeValor.IntervaloDeTiempo(inicio, fin);
        }

    }
}
