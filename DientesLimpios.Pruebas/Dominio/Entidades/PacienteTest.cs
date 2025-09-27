﻿using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Pruebas.Dominio.Entidades
{
    [TestClass]
    public class PacienteTest
    {

        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_NombreNulo_LanzaExcepcion()
        {
            var email = new Email("alpha0202@gmail.com");
            new Paciente(null!, email);
        }

        [TestMethod]
        [ExpectedException(typeof(ExcepcionesReglaDeNegocio))]
        public void Construir_EmailNulo_LanzaExcepcion()
        {
            Email email = new Email(null!);
            new Paciente("edwin", email);
        }
    }
}
