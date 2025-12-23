using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Pruebas.Aplicacion.CasoDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoActualizarConsultorioTest
    {

        private IRepositorioConsultorios repositorio;
        private IUnidadDeTrabajo unidadDeTrabajo;
        private CasoDeUsoActualizarConsultorio casoDeUsoActualizarConsultorio;


        [TestInitialize]
        public void setup()
        {
            repositorio = Substitute.For<IRepositorioConsultorios>();
            unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            casoDeUsoActualizarConsultorio = new CasoDeUsoActualizarConsultorio(repositorio, unidadDeTrabajo);
        }


        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_ActualizarNombreYPersiste()
        {
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio
            {
                Id = id,
                Nombre = "Consultorio B"
            };

            repositorio.ObtenerPorId(id).Returns(consultorio);

            await casoDeUsoActualizarConsultorio.Handle(comando);

            await repositorio.Received(1).Actualizar(consultorio);
            await unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            var id = Guid.NewGuid();
            var comando = new ComandoActualizarConsultorio
            {
                Id = id,
                Nombre = "Consultorio B"
            };
            repositorio.ObtenerPorId(id).ReturnsNull();
            await casoDeUsoActualizarConsultorio.Handle(comando);


        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcionAlActualizar_LlamarReversarLanzarExcepcion()
        { 
            var consultorio = new Consultorio("Consultorio A");
            var id = consultorio.Id;
            var comando = new ComandoActualizarConsultorio
            {
                Id = id,
                Nombre = "Consultorio B"
            };

            repositorio.ObtenerPorId(id).Returns(consultorio);
            repositorio.Actualizar(consultorio).Throws(new InvalidOperationException("error al actualizar"));

            await Assert.ThrowsExceptionAsync<InvalidOperationException>( () => casoDeUsoActualizarConsultorio.Handle(comando) );
            await unidadDeTrabajo.Received(1).Reversar();


        }


    }
}
