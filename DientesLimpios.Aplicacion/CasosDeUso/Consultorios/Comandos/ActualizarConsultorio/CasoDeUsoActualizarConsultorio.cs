using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class CasoDeUsoActualizarConsultorio : IRequestHandler<ComandoActualizarConsultorio>
    {
        private readonly IRepositorioConsultorios _repositorioConsultorios;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CasoDeUsoActualizarConsultorio(IRepositorioConsultorios repositorioConsultorios, IUnidadDeTrabajo unidadDeTrabajo)
        {
            _repositorioConsultorios = repositorioConsultorios;
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task Handle(ComandoActualizarConsultorio request)
        {
            var consultorio = await _repositorioConsultorios.ObtenerPorId(request.Id);

            if (consultorio == null)
            {
                throw new ExcepcionNoEncontrado();
            }
            consultorio.ActualizarNombre(request.Nombre);

            try
            {
                await _repositorioConsultorios.Actualizar(consultorio);
                await _unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                await _unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
