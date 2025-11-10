using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());

            var validador = _serviceProvider.GetService(tipoValidador);

            if (validador is not null)
            {
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var TareaValidar = await (Task<FluentValidation.Results.ValidationResult>)metodoValidar!.Invoke(validador, [request, CancellationToken.None])!;

                //await TareaValidar.ConfigureAwait(false);

                //var resultadoValidacion = TareaValidacion.GetType().GetProperty("Result");
                //var resultadoValidacionValue = (ValidationResult)resultadoValidacion!.GetValue(TareaValidacion)!;

                if (!TareaValidar.IsValid)
                {
                    throw new ExcepcionValidacion(TareaValidar);
                }


            }




            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = _serviceProvider.GetService(tipoCasoDeUso);


            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un caso de uso para la solicitud de tipo {request.GetType().Name}");
            }

            var metodoHandle = tipoCasoDeUso.GetMethod("Handle")!;
            return await (Task<TResponse>)metodoHandle.Invoke(casoDeUso, [request]);

        }
    }
}
