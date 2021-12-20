using DAL.RepoInterfaces;
using Domain;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService : IProductService
    {
        IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public void AddProduct(Product product)
        {
            _repository.Add(product);
        }

        public void DeleteProduct(int id)
        {
            Product product = _repository.GetSingle(id);
            _repository.Delete(product);
        }

        public Product GetProduct(int id)
        {
            return _repository.GetSingle(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _repository.GetAll();
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }
    }
}
