using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orleans.Web.Grains
{
    public interface IOrderGrain : IGrainWithGuidKey
    {
        Task AddProduct(Order order);
    }
    public class OrderGrain : Grain<OrderState>, IOrderGrain
    {
        public async Task AddProduct(Order order)
        {
            State = new OrderState
            {
                Products = order.Products
            };
            await WriteStateAsync();
        }
    }
    public class OrderState
    {
        public List<Product> Products { get; set; }
    }
}
