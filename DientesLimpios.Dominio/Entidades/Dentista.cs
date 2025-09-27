using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Dentista
    {
        public Guid Id { get;private set; }
        public string Nombre { get; private set; } = null!;
        public Email Email { get; private set; } = null!;


        public Dentista(string nombre, Email email)
        {

            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionesReglaDeNegocio($"El {nameof(nombre)} es obligatorio.");
            }

           

            Nombre = nombre;
            Email = email;
            Id = Guid.CreateVersion7();
        }

    }
}
