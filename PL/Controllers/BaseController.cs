using DAL.Context;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class BaseController : Controller
    {
        BlogContext ent = new BlogContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Kullanılacak genel verilerin ViewBaglerin tanımlanması
            base.OnActionExecuting(filterContext);
        }
    }
}