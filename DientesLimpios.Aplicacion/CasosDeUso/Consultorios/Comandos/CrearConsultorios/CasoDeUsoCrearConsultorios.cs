﻿using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio
{
    public class CasoDeUsoCrearConsultorios
    {
        private readonly IRepositorioConsultorio _repositorio;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public CasoDeUsoCrearConsultorios(IRepositorioConsultorio repositorio, IUnidadDeTrabajo unidadDeTrabajo)
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
