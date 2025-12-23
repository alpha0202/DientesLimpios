using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
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
    public class CasoDeUsoBorrarConsultorioTest
    {

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios _repositorioConsultorios;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private CasoDeUsoBorrarConsultorio _casoDeUsoBorrarConsultorio;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    

    [TestInitialize]
        public void Setup()
        {
            _repositorioConsultorios = Substitute.For<IRepositorioConsultorios>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _casoDeUsoBorrarConsultorio = new CasoDeUsoBorrarConsultorio(_repositorioConsultorios, _unidadDeTrabajo);
        }

        [TestMethod]
        public async Task Handle_CuandoConsultorioExiste_BorraConsultorioYPersiste()
        {
           // Arrange
            var consultorioId = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = consultorioId };
            var consultorio = new Consultorio("Consultorio A");
            _repositorioConsultorios.ObtenerPorId(consultorioId).Returns(consultorio);
            // Act
            await _casoDeUsoBorrarConsultorio.Handle(comando);
            // Assert
            await _repositorioConsultorios.Received(1).Borrar(consultorio);
            await _unidadDeTrabajo.Received(1).Persistir();
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_CuandoConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            // Arrange
            var consultorioId = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = consultorioId };
            _repositorioConsultorios.ObtenerPorId(consultorioId).ReturnsNull();
            
            // Act & Assert
            
            await _casoDeUsoBorrarConsultorio.Handle(comando);
            
        }

        [TestMethod]
        public async Task Handle_CuandoOcurreExcepcion_LlamarReversarYLanzarExcepcion()
        {
            // Arrange
            var consultorioId = Guid.NewGuid();
            var comando = new ComandoBorrarConsultorio { Id = consultorioId };
            var consultorio = new Consultorio("Consultorio A");
            _repositorioConsultorios.ObtenerPorId(consultorioId).Returns(consultorio);
            _repositorioConsultorios.Borrar(consultorio).Throws(new InvalidOperationException("falló borrado"));
            // Act & Assert

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>_casoDeUsoBorrarConsultorio.Handle(comando));


            await _unidadDeTrabajo.Received(1).Reversar();
        }

        }
}
