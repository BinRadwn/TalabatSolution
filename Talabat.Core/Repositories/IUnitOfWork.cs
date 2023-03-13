using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenaricRepository<TEntity> Repository <TEntity>() where TEntity :BaseEntity;

        Task<int> Compelet();
    } 
}
