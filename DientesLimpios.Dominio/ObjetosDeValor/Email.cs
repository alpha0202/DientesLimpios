using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    public class Email
    {

        public string Valor { get; } = null!;
        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ExcepcionesReglaDeNegocio($"El {nameof(email)} es obligatorio.");
            }

            if (email is null)
            {
                throw new ExcepcionesReglaDeNegocio($"El {nameof(email)} es obligatorio.");
            }


            if (!email.Contains("@"))
            {
                throw new ExcepcionesReglaDeNegocio($"El {nameof(email)} no es válido.");
            }

            Valor = email;
        }

    }
}
