using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class CitaTest
    {

        private Guid _pacienteId = Guid.NewGuid();
        private Guid _dentistaId = Guid.NewGuid();
        private Guid _consultorioId = Guid.NewGuid();
        private IntervaloDeTiempo _intervaloDeTiempo = new IntervaloDeTiempo(DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

        [TestMethod]
        public void Construir_CitaValida_EstadoProgramada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervaloDeTiempo);


            Assert.AreEqual(_pacienteId, cita.PacienteId);
            Assert.AreEqual(_dentistaId, cita.DentistaId);
            Assert.AreEqual(_consultorioId, cita.ConsultorioId);
            Assert.AreEqual(_intervaloDeTiempo, cita.IntervaloDeTiempo);
            Assert.AreEqual(EstadoCita.programada, cita.Estado);
            Assert.AreNotEqual(Guid.Empty, cita.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_CitaConFechaInicioEnElPasada_LanzaExcepcion()
        {
            var intervaloDeTiempo = new IntervaloDeTiempo(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, intervaloDeTiempo);
        }

        [TestMethod]
        public void Cancelar_CitaProgramada_CambiaEstadoCancelada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervaloDeTiempo);
            cita.Cancelar();
            Assert.AreEqual(EstadoCita.cancelada, cita.Estado);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Cancelar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervaloDeTiempo);
            cita.Cancelar();
            cita.Cancelar();
        }

        [TestMethod] 
        public void Completar_CitaProgramada_CambiaEstadoCompletada()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervaloDeTiempo);
            cita.Completar();
            Assert.AreEqual(EstadoCita.completada, cita.Estado);
        }
        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Completar_CitaNoProgramada_LanzaExcepcion()
        {
            var cita = new Cita(_pacienteId, _dentistaId, _consultorioId, _intervaloDeTiempo);
            cita.Cancelar();
            cita.Completar();
        }
    }
}
