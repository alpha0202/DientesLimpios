using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class ValidadorComandoActualizarConsultorio : AbstractValidator<ComandoActualizarConsultorio>
    {

        public ValidadorComandoActualizarConsultorio()
        {
            RuleFor(p => p.Nombre)
             .NotEmpty().WithMessage("El {PropertyName} es obligatorio.")
             .MaximumLength(150).WithMessage("La Longitud del campo {PropertyName} debe ser menor o igual a {MaxLength}");
        }
    }
}
