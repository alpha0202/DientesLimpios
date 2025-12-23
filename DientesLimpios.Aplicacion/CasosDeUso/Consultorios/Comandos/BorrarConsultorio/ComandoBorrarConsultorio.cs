using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio
{
    public class ComandoBorrarConsultorio : IRequest
    {
        public Guid Id { get; set; }

    }
}
