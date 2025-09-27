using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DientesLimpios.Dominio.Entidades
{
    public class Paciente
    {
        public Guid Id { get;private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        public Paciente(string nombre, Email email)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionesReglaDeNegocio($"El {nameof(nombre)} es obligatorio.");
            }

            

            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }



    }
}
