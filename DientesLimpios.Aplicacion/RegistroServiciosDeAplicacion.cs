using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.ObtenerDetalleConsulta;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion
{
    public static class RegistroServiciosDeAplicacion
    {

        public static IServiceCollection AgregarServiciosDeAplicacion(this IServiceCollection services)
        {
            // Aquí puedes registrar otros servicios de la capa de aplicación si es necesario

            services.AddTransient<IMediator, MediadorSimple>();
            services.AddScoped<IRequestHandler<ComandoCrearConsultorios, Guid>, CasoDeUsoCrearConsultorios>();
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>, CasoDeUsoObtenerDetalleConsultorio>();
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios, List<ConsultorioListadoDTO>>, CasoDeUsoObtenerListadoConsultorios>();



            return services;
        }

    }
}
