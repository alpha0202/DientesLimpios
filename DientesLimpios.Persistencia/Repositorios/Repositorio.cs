using DientesLimpios.Aplicacion.Contratos.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Repositorios
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly DientesLimpiosDbContext _limpiosDbContext;

        public Repositorio(DientesLimpiosDbContext limpiosDbContext)
        {
            _limpiosDbContext = limpiosDbContext;
        }

        public Task Actualizar(T entidad)
        {
            _limpiosDbContext.Update(entidad);
            return Task.CompletedTask;
        }

        public Task<T> Agregar(T entidad)
        {
            _limpiosDbContext.Add(entidad);
            return Task.FromResult(entidad);    
        }

        public Task Borrar(T entidad)
        {
           _limpiosDbContext.Remove(entidad);
              return Task.CompletedTask;
        }

        public async Task<T?> ObtenerPorId(Guid id)
        {
            
            return await _limpiosDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> ObtenerTodos()
        {
            return await _limpiosDbContext.Set<T>().ToListAsync();
        }
    }
}
