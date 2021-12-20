using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain
{
    public class Product
    {
        [Key]
        public int Id {  get; set; }  
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [JsonIgnore]
        public List<Image> Images { get; set; }
    }
}
