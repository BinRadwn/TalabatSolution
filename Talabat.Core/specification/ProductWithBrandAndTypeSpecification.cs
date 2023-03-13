using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {

        //This Constructor is Used For Get All Products
        public ProductWithBrandAndTypeSpecification(ProductspecParam productParam)
            : base(p =>

                     (string.IsNullOrEmpty(productParam.Search) || p.Name.ToLower().Contains(productParam.Search)) &&
                     (!productParam.BrandId.HasValue || p.ProductBrandId == productParam.BrandId.Value) &&
                     (!productParam.TypeId.HasValue || p.ProductTypeId == productParam.TypeId.Value)


            )
        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);
            AddOrderBy(P => P.Name);


            //Take And Skip
                                    //skip                                     take
            ApplyPagination(productParam.PageSize * (productParam.PageIndex - 1), productParam.PageSize);

            //OrderBy
            if (!string.IsNullOrEmpty(productParam.Sort))
            {
                switch (productParam.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;

                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

        }

        //This Constructor is Used For Get a Specific Product
        public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.productType);
            AddInclude(P => P.productBrand);
        }
    }
}
