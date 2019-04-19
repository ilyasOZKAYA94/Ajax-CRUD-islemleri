using DAL.Migrations;
using Entity.Entity;
using Entity.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace DAL.Context
{
    public class BlogContext : IdentityDbContext<ApplicationUser>
    {
        public BlogContext() : base("BlogContext")
        {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Configuration>("BlogContext"));
        }


        public virtual DbSet<Post> Posts { get; set; }
    }
}
