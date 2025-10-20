using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorios : IRequestHandler<ComandoCrearConsultorios, Guid>
    {
        private readonly IRepositorioConsultorio _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IValidator<ComandoCrearConsultorios> _validator;

        public CasoDeUsoCrearConsultorios(IRepositorioConsultorio repositorio, 
                                          IUnidadDeTrabajo unidadDeTrabajo, 
                                          IValidator<ComandoCrearConsultorios> validator)
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
            _validator = validator;
        }

        public async Task<Guid> Handle(ComandoCrearConsultorios comando)
        {
            
            var resultadoValidacion = await _validator.ValidateAsync(comando);
            if (!resultadoValidacion.IsValid)
            {
                
                throw new ExcepcionValidacion(resultadoValidacion);
            }

            var consultorio = new Consultorio(comando.Nombre);
            try
            {
                 var respuesta = await _repositorio.Agregar(consultorio);
                await _unidadDeTrabajo.Persistir();
                 return respuesta.Id;

            }
            catch (Exception)
            {

                await _unidadDeTrabajo.Reversar();
                throw;
            }
            
            
        }


    }
}
