using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio
{
    public class CasoDeUsoBorrarConsultorio : IRequestHandler<ComandoBorrarConsultorio>
    {
        private readonly IRepositorioConsultorios _repositorioConsultorios;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CasoDeUsoBorrarConsultorio(IRepositorioConsultorios repositorioConsultorios, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorioConsultorios = repositorioConsultorios;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(ComandoBorrarConsultorio request)
        {
            var consultorio = await _repositorioConsultorios.ObtenerPorId(request.Id);

            if(consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }
            try
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                _repositorioConsultorios.Borrar(consultorio);
                _unidadDeTrabajo.Persistir();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
