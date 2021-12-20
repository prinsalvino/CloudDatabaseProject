using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain
{
    public class User
    {
        [Key]

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public UserRoles UserRoles { get; set; }
        
        public DateTime CreatedDate {  get; set; } = DateTime.Now;

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public List<Order> Orders { get; set; }

        public User()
        {

        }

        public User(int id, string name, string email, UserRoles userRoles, DateTime createdDate, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            UserRoles = userRoles;
            CreatedDate = createdDate;
            Password = password;
        }
    }
}
