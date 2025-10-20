using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Excepciones
{
    public class ExcepcionValidacion : Exception
    {
        public List<string> ErroresValidacion { get; set; } = [];

        public ExcepcionValidacion(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                ErroresValidacion.Add(error.ErrorMessage);
            }
        }

    }
}
