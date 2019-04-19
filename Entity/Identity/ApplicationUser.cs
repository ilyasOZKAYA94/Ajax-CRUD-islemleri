using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        [StringLength(255)]
        public string UserPicture { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
