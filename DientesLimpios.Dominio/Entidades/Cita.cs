using DientesLimpios.Dominio.Enums;
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Dominio.Entidades
{
    public class Cita
    {

        public Guid Id { get; private set; }
        public Guid PacienteId { get; private set; }
        public Guid DentistaId { get; private set; }
        public Guid ConsultorioId { get; private set; }
        public EstadoCita Estado { get; private set; }
        public IntervaloDeTiempo    IntervaloDeTiempo { get; private set; }
        public Paciente? Paciente { get; private set; }

        public Dentista? Dentista { get; private set; }

        public Consultorio? Consultorio { get; private set; }


        public Cita(Guid pacienteId, Guid dentistaId, Guid consultorioId, IntervaloDeTiempo intervaloDeTiempo)
        {

           

            if (intervaloDeTiempo.Inicio < DateTime.UtcNow)
            {
                throw new ExcepcionesReglaDeNegocio($"La fecha de inicio no puede ser anterior a la fecha actual.");
            }

            PacienteId = pacienteId;
            DentistaId = dentistaId;
            ConsultorioId = consultorioId;
            IntervaloDeTiempo = intervaloDeTiempo;
            Estado = EstadoCita.programada;
            Id = Guid.CreateVersion7();

        }

        public void Cancelar()
        {
            if (Estado != EstadoCita.programada)
            {
                throw new ExcepcionesReglaDeNegocio($"Solo se pueden cancelar citas programadas.");
            }

            Estado = EstadoCita.cancelada;
        }

        public void Completar()
        {
            if (Estado != EstadoCita.programada)
            {
                throw new ExcepcionesReglaDeNegocio($"Solo se pueden completar citas programadas.");
            }
            Estado = EstadoCita.completada;

        }
    }
}

