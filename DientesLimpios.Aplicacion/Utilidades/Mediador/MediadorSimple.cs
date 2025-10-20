using DientesLimpios.Aplicacion.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    public class MediadorSimple : IMediator
    {
        private readonly IServiceProvider _serviceProvider;

        public MediadorSimple(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }



        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = _serviceProvider.GetService(tipoCasoDeUso);


            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un caso de uso para la solicitud de tipo {request.GetType().Name}");
            }

            var metodoHandle = tipoCasoDeUso.GetMethod("Handle");
            return await (Task<TResponse>)metodoHandle.Invoke(casoDeUso, new object[] { request });

        }
    }
}
