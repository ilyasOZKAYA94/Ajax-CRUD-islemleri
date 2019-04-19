using DAL.Context;
using Entity.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BLL.Identity
{
    public static class IdentityTools
    {
        public static UserStore<ApplicationUser> NewUserStore() => new UserStore<ApplicationUser>(new BlogContext());
        public static UserManager<ApplicationUser> NewUserManager() => new UserManager<ApplicationUser>(NewUserStore());

        public static RoleStore<ApplicationRole> NewRoleStore() => new RoleStore<ApplicationRole>(new BlogContext());
        public static RoleManager<ApplicationRole> NewRoleManager() => new RoleManager<ApplicationRole>(NewRoleStore());
    }
}
