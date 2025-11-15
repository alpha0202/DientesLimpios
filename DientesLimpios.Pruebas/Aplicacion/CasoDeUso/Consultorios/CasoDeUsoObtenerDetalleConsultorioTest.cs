using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.ObtenerDetalleConsulta;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasoDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerDetalleConsultorioTest
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private IRepositorioConsultorios _repositorio;
        private CasoDeUsoObtenerDetalleConsultorio _casoDeUso;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            _repositorio = Substitute.For<IRepositorioConsultorios>();
            _casoDeUso = new CasoDeUsoObtenerDetalleConsultorio(_repositorio);
        }

        [TestMethod]
        public async Task Handle_ConsultorioExiste_RetornaDTO()
        {
            //Arrange
            var consultorio = new Consultorio("Consultorio A");
            var idConsultorio = consultorio.Id;
            var consulta = new ConsultaObtenerDetalleConsultorio
            {
                Id = idConsultorio
            };

            _repositorio.ObtenerPorId(idConsultorio).Returns(consultorio);

            //Act
            var resultado = await _casoDeUso.Handle(consulta);

            //Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(idConsultorio, resultado.Id);
            Assert.AreEqual("Consultorio A", resultado.Nombre);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionNoEncontrado))]
        public async Task Handle_ConsultorioNoExiste_LanzaExcepcionNoEncontrado()
        {
            //Arrange
            var idConsultorio = Guid.NewGuid();
            var consulta = new ConsultaObtenerDetalleConsultorio
            {
                Id = idConsultorio
            };
            _repositorio.ObtenerPorId(idConsultorio).ReturnsNull();
            //Act & Assert
            
            
            await _casoDeUso.Handle(consulta);
        
        }
    }
}