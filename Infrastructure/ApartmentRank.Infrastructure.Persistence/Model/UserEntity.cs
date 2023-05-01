using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApartmentRank.Infrastructure.Persistence.Model
{
    public class UserEntity
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
