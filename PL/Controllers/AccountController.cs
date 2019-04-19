using BLL.Identity;
using Entity.Identity;
using Entity.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)  //Model ile gelen verinin Validation kontrolü
                return View(model);
            var usermanager = IdentityTools.NewUserManager();

            ApplicationUser kullanici = usermanager.FindByEmail(model.Email);
            if (kullanici == null) //Email sistemde kayıtlımı kontrolü
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı kayıtlı değil!");
                return View(model);
            }
            else if (!usermanager.CheckPassword(kullanici, model.Password)) //Şifre kontrolü
            {
                ModelState.AddModelError("", "Bilgilerinizi kontrol ediniz!");
                return View(model);
            }
            else
            {
                var role = usermanager.GetRoles(kullanici.Id);
                var authManager = HttpContext.GetOwinContext().Authentication;
                var identity = usermanager.CreateIdentity(kullanici, "ApplicationCookie");
                var authProperty = new AuthenticationProperties //Authentication işlemleri
                {
                    IsPersistent = model.RememberMe
                };
                authManager.SignIn(authProperty, identity);
                Session["User"] = kullanici.Name;               
                return Redirect(string.IsNullOrEmpty(model.returnUrl) ? "/" : model.returnUrl);
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)//Model ile gelen verinin Validation kontrolü
                return View(model);
            
            var usermanager = IdentityTools.NewUserManager();

            var kullanici = usermanager.FindByEmail(model.Email);
            if (kullanici != null)//Email sistemde kayıtlımı kontrolü
            {
                ModelState.AddModelError("", "Bu email sistemde kayıtlı!");
                return View(model);
            }
            ApplicationUser user = new ApplicationUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.UserName = model.Username;
            if (model.PictureUpload == null)// Kullanıcı resim eklemezse Default resim ataması
                user.UserPicture = "avatar.png";
            else
            {//Resimin Database ve İmages klasörüne yazılması 
                string filename = model.PictureUpload.FileName;
                string imagePath = Server.MapPath("/images/" + filename);
                model.PictureUpload.SaveAs(imagePath);
                user.UserPicture = filename;
            }
            var result = usermanager.Create(user, model.Password);//User Kaydı

            if (result.Succeeded)
            {
               // usermanager.AddToRole(user.Id, "User");   istendiğinde rol eklenebilir
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            return View(model);
        }
        [Authorize]
        public ActionResult LogOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}