using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.CasoDeUso.Consultorios
{
    [TestClass]
    public class CasoDeUsoObtenerListadoConsultoriosTest
    {

        private IRepositorioConsultorios _repositorioConsultorios;
        private CasoDeUsoObtenerListadoConsultorios _casoDeUso;



        [TestInitialize]
        public void Setup()
        {
            _repositorioConsultorios = NSubstitute.Substitute.For<IRepositorioConsultorios>();
            _casoDeUso = new CasoDeUsoObtenerListadoConsultorios(_repositorioConsultorios);
        }

        [TestMethod]
        public async Task Handle_DeberiaRetornarListadoDeConsultorios()
        {
            // Arrange
            var consultorios = new List<Consultorio>
            {
                new Consultorio("Consultorio A"),
                new Consultorio("Consultorio B")
            };
            _repositorioConsultorios.ObtenerTodos().Returns(consultorios);

            // Act
            var expectedListado = consultorios.Select(c => new ConsultorioListadoDTO
            {
                Id = c.Id,
                Nombre = c.Nombre
            }).ToList();

            
            var resultado = await _casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());

            // Assert
            Assert.AreEqual(expectedListado.Count, resultado.Count);


            for (int i = 0; i < expectedListado.Count; i++)
            {
                Assert.AreEqual(expectedListado[i].Id, resultado[i].Id);
                Assert.AreEqual(expectedListado[i].Nombre, resultado[i].Nombre);
            }

           
        }


        [TestMethod]
        public async Task Handle_CuandoNoHayConsultorios_DeberiaRetornarListadoVacio()
        {
            // Arrange
            var consultorios = new List<Consultorio>();
            _repositorioConsultorios.ObtenerTodos().Returns(consultorios);
            // Act
            var resultado = await _casoDeUso.Handle(new ConsultaObtenerListadoConsultorios());
            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(0, resultado.Count);
        }
    }
}
