using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.specification;

namespace Talabat.Repository.Data
{
    public class specificationEvaluator<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> Spec)
        {
            var Query = inputQuery;

            if (Spec.Criteria != null)
                Query = Query.Where(Spec.Criteria);


            if (Spec.orderBy != null)
                Query = Query.OrderBy(Spec.orderBy);

            if (Spec.orderByDescending != null)
                Query = Query.OrderByDescending(Spec.orderByDescending);

            if (Spec.IsPaginationEnabled)
                Query = Query.Skip(Spec.Skip).Take(Spec.Take); 


            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, Include) => CurrentQuery.Include(Include));


            return Query;
        }
    }
}
