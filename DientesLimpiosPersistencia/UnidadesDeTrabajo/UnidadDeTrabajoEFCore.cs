using DientesLimpios.Aplicacion.Contratos.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.UnidadesDeTrabajo
{
    public class UnidadDeTrabajoEFCore : IUnidadDeTrabajo
    {
        private readonly DientesLimpiosDbContext _dientesLimpiosDbContext;

        public UnidadDeTrabajoEFCore(DientesLimpiosDbContext dientesLimpiosDbContext)
        {
            _dientesLimpiosDbContext = dientesLimpiosDbContext;
        }

        public async Task Persistir()
        {
            await _dientesLimpiosDbContext.SaveChangesAsync();
        }

        public Task Reversar()
        {
            return Task.CompletedTask;
        }
    }
}
