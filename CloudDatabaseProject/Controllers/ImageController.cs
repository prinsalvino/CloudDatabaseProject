using AutoMapper;
using CloudDatabaseProject.DTO;
using CloudDatabaseProject.Infrastructure;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CloudDatabaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        IImageService imageService;
        IMapper _mapper;
        ILogger logger;
        public ImageController(IImageService imageService, IMapper mapper, ILogger logger)
        {
            this.imageService = imageService;
            _mapper = mapper;
            this.logger = logger;
        }
        // GET: api/<ImageController>
        [HttpGet]
        public IEnumerable<Image> Get()
        {
            try
            {
                return imageService.GetImages();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        // GET api/<ImageController>/5
        [HttpGet("{id}")]
        public Image Get(int id)
        {
            try
            {
                return imageService.GetImage(id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
        }

        // POST api/<ImageController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImageDTO imageDTO)
        {
            try
            {
                var image = _mapper.Map<Image>(imageDTO);

                const string ImageToUpload = "product.jpg";
                await imageService.AddImage(image, ImageToUpload);
                return new OkResult();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return null;
            }
           
        }

        // PUT api/<ImageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ImageDTO value)
        {
            try
            {
                Image img = imageService.GetImage(id);
                img.ProductId = value.ProductId;
                imageService.UpdateImage(img);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
       
        }

        // DELETE api/<ImageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                imageService.DeleteImage(id);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
    }
}
