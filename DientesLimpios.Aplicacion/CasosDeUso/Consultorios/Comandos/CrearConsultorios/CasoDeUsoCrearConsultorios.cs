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
        private readonly IRepositorioConsultorios _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
     

        public CasoDeUsoCrearConsultorios(IRepositorioConsultorios repositorio, 
                                          IUnidadDeTrabajo unidadDeTrabajo
                                          
                                          )
        {
            _repositorio = repositorio;
            _unidadDeTrabajo = unidadDeTrabajo;
           
        }

        public async Task<Guid> Handle(ComandoCrearConsultorios comando)
        {
                        
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
