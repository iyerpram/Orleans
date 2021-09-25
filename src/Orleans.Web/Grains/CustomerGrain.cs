using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.Web.Grains
{
    public interface ICustomerGrain : IGrainWithGuidKey
    {
        Task OrderProduct(Order order);
    }
    public class CustomerGrain : Grain, ICustomerGrain
    {
        public IClusterClient ClusterClient { get; }

        public CustomerGrain(IClusterClient clusterClient)
        {
            ClusterClient = clusterClient;
        }        

        public async Task OrderProduct(Order order)
        {
            await ClusterClient.GetGrain<OrderGrain>(Guid.NewGuid()).AddProduct(order);
        }
    }    

    public class Order
    {
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
    }
}
