using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entity
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string PostTitle { get; set; }
        [Required]
        [StringLength(999)]
        public string PostContent { get; set; }
        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [Required]
        public bool IsDeleted { get; set; } = false;
        public string UserPıcture { get; set; }
    }
}
