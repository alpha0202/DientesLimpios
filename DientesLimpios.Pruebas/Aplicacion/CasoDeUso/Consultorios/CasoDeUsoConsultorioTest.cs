using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasoDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoConsultorioTest
    {
        private IRepositorioConsultorio _repositorio;
        private IUnidadDeTrabajo _unidadDeTrabajo;
        private IValidator<ComandoCrearConsultorios> _validator;
        private CasoDeUsoCrearConsultorios _casoDeUsoCrearConsultorios;

        [TestInitialize]
        public void SetUp()
        {
            _repositorio = Substitute.For<IRepositorioConsultorio>();
            _unidadDeTrabajo = Substitute.For<IUnidadDeTrabajo>();
            _validator = Substitute.For<IValidator<ComandoCrearConsultorios>>();
            _casoDeUsoCrearConsultorios = new CasoDeUsoCrearConsultorios(_repositorio, _unidadDeTrabajo, _validator);
        }


        [TestMethod]
        public async Task Handle_ComandoValido_ObtenemosIdConsultorio()
        {
            // Arrange
            var comando = new ComandoCrearConsultorios
            {
                Nombre = "Consultorio Central"
            };

            _validator.ValidateAsync(comando).Returns(new ValidationResult());


            var consultorioCreado = new Consultorio("Consultorio A");
            _repositorio.Agregar(Arg.Any<Consultorio>()).Returns(consultorioCreado);
            // Act
            var resultado = await _casoDeUsoCrearConsultorios.Handle(comando);
            // Assert
            await _validator.Received(1).ValidateAsync(comando);
            await _repositorio.Received(1).Agregar(Arg.Any<Consultorio>());
            await _unidadDeTrabajo.Received(1).Persistir();
            Assert.AreNotEqual(Guid.Empty, resultado);
        }

        [TestMethod]
        public async Task Handle_ComandoInvalido_LanzaExcepcion()
        {
            // Arrange
            var comando = new ComandoCrearConsultorios
            {
                Nombre = "" // Nombre inválido
            };

            var resultadoValidacion = new ValidationResult(new[]
            {
                new ValidationFailure("Nombre", "El nombre no puede estar vacío.")
            });

           
            _validator.ValidateAsync(comando).Returns(resultadoValidacion);
           
            // Act & Assert
            await Assert.ThrowsExceptionAsync<ExcepcionValidacion>(async () =>
            {
                await _casoDeUsoCrearConsultorios.Handle(comando);
            });

            await _repositorio.DidNotReceive().Agregar(Arg.Any<Consultorio>());
           

        }

        [TestMethod]
        public async Task Handle_ErrorEnRepositorio_LanzaExcepcionYRollBack()
        {
            // Arrange
            var comando = new ComandoCrearConsultorios
            {
                Nombre = "Consultorio Central"
            };

            _repositorio.Agregar(Arg.Any<Consultorio>()).Returns<Task<Consultorio>>(x => { throw new Exception("Error en el repositorio"); });
            _validator.ValidateAsync(comando).Returns(new ValidationResult());
            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await _casoDeUsoCrearConsultorios.Handle(comando);
            });
            await _unidadDeTrabajo.Received(1).Reversar();
        }
    }
}