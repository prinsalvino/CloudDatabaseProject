using DAL.RepoInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ShoppingContext context) : base(context)
        {
        }
    }
}
