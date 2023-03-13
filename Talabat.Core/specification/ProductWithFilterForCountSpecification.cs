using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductspecParam productParam)
    : base(p =>

             (string.IsNullOrEmpty(productParam.Search) || p.Name.ToLower().Contains(productParam.Search)) &&

             (!productParam.BrandId.HasValue || p.ProductBrandId == productParam.BrandId.Value) &&
             (!productParam.TypeId.HasValue || p.ProductTypeId == productParam.TypeId.Value)

           )
        {
        }
    }
}
