using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorio;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.CrearConsultorios
{
    public class ValidadorComandoCrearConsultorio : AbstractValidator<ComandoCrearConsultorios>
    {
        public ValidadorComandoCrearConsultorio()
        {
            RuleFor(x => x.Nombre)
             .NotEmpty().WithMessage("El campo {propertyName} del consultorio no puede estar vacío.");
                
        }

    }
}
