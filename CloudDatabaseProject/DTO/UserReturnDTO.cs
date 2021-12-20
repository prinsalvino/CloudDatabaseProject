using Domain;
using System;

namespace CloudDatabaseProject.DTO
{
    public class UserReturnDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
