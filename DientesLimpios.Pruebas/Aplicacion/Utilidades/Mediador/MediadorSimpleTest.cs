using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Aplicacion.Utilidades.Mediador
{
    [TestClass]
    public class MediadorSimpleTest
    {

        public class RequestFalso : IRequest<string> { }

        public class RequestHandlerFalso : IRequestHandler<RequestFalso, string>
        {
            public Task<string> Handle(RequestFalso request)
            {
                return Task.FromResult("Respuesta Falsa correcta.");
            }
        }

        [TestMethod]
        public async Task Send_LLamaMetodoHandler()
        {
            var request = new RequestFalso();

            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();

            var serviceProviderMock = Substitute.For<IServiceProvider>();

            serviceProviderMock.GetService(typeof(IRequestHandler<RequestFalso, string>))
                               .Returns(casoDeUsoMock);

            var mediador = new MediadorSimple(serviceProviderMock);
            var resultado = await mediador.Send(request);
            await casoDeUsoMock.Received(1).Handle(request);

        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionDeMediador))]
        public async Task Send_CasoDeUsoNoEncontrado_LanzaExcepcion()
        {
            var request = new RequestFalso();
            var casoDeUsoMock = Substitute.For<IRequestHandler<RequestFalso, string>>();
            var serviceProviderMock = Substitute.For<IServiceProvider>();

            var mediador = new MediadorSimple(serviceProviderMock);
            
            
            var resultado = await mediador.Send(request);

           


        }
    }
}
