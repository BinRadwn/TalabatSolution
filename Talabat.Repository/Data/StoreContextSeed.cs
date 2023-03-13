using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.productBrands.Any())
                {
                    var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    foreach (var brand in brands)
                        context.Set<ProductBrand>().Add(brand);
                }
                if (!context.productTypes.Any())
                {
                    var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                    foreach (var type in Types)
                        context.Set<ProductType>().Add(type);
                }
                if (!context.products.Any())
                {
                    var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach (var product in Products)
                        context.Set<Product>().Add(product);
                }
                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    foreach (var deliveryMethod in DeliveryMethods)
                        context.Set<DeliveryMethod>().Add(deliveryMethod);
                }


                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var Logger = loggerFactory.CreateLogger<StoreContextSeed>();
                Logger.LogError(ex, ex.Message);

            }

        }

    }
}
