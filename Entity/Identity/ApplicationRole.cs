using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace Entity.Identity
{
    public class ApplicationRole : IdentityRole
    {
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
