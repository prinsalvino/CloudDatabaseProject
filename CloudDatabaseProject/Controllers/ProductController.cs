using AutoMapper;
using CloudDatabaseProject.DTO;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudDatabaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        IMapper _mapper;
        ILogger logger;
        public ProductController(IProductService productService, IMapper mapper, ILogger logger)
        {
            _mapper = mapper;
            _productService = productService;
            this.logger = logger;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            try
            {
                var products = _productService.GetProducts();
                return products;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }

        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            try
            {
                return _productService.GetProduct(id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                var product = _mapper.Map<Product>(productDTO);
                _productService.AddProduct(product);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
           
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductDTO value)
        {
            try
            {
                var product = _productService.GetProduct(id);
                product.Name = value.Name;
                product.Price = value.Price;
                _productService.UpdateProduct(product);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                _productService.DeleteProduct(id);

            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
