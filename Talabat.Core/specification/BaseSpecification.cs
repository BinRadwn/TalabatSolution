using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        //Property
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> orderBy { get; set; }
        public Expression<Func<T, object>> orderByDescending { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationEnabled { get; set; }

        //Constructors

        //GetAllProductsUsingSpecification
        public BaseSpecification()
        {

        }

        //GetAllProductByIdUsingSpecification
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
        }

        //Method
        public void AddInclude(Expression<Func<T, object>> expression)
        {
            Includes.Add(expression);
        }


        //Just setter
        public void AddOrderBy(Expression<Func<T, object>> orderby)
        {
            orderBy = orderby;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderbydesc)
        {
            orderByDescending = orderbydesc;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;    
        }


    }
}
