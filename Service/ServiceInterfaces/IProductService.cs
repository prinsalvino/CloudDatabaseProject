using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IProductService
    {
        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

        public Product GetProduct(int id);

        public void DeleteProduct(int id);

        public IEnumerable<Product> GetProducts();
    }
}
