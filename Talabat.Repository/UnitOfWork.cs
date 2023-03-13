using Pipelines.Sockets.Unofficial.Arenas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;


        //Constructor
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        //Methods
        public async Task<int> Compelet()
            => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var Type = typeof(TEntity).Name;

            if (!_repositories.Contains(Type))
            {
                var Repo = new GenaricRepository<TEntity>(_context);
                _repositories.Add(Type, Repo);
            }
            return (IGenaricRepository<TEntity>)_repositories[Type];
        }
    }
}
