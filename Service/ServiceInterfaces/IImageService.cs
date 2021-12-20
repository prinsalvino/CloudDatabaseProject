using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ServiceInterfaces
{
    public interface IImageService
    {
        public Task AddImage(Image img, string ImageToUpload);

        public void UpdateImage(Image image);

        public Image GetImage(int id);

        public void DeleteImage(int id);

        public IEnumerable<Image> GetImages();
    }
}
