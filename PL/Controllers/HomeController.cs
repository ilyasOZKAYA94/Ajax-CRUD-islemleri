using BLL.Repository;
using DAL.Context;
using Entity.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class HomeController : BaseController
    {
        Repository<Post> repoP = new Repository<Post>(new BlogContext());
        public ActionResult Index()
        {
            ViewBag.Post = repoP.GetAll(x=>x.IsDeleted==false).Reverse();  //Ana ekranda bütün postların tarihe göre sıralı görüntülenmesi
            return View();
        }

        
    }
}