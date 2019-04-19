using System;
using System.Linq;
using System.Web.Mvc;
using BLL.Repository;
using DAL.Context;
using Entity.Entity;
using Entity.Identity;

namespace PL.Controllers
{
    public class ProfileController : BaseController
    {
        Repository<Post> repoP = new Repository<Post>(new BlogContext());
        BlogContext ent = new BlogContext();

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.MyPost = repoP.GetAll(x=>x.UserName==HttpContext.User.Identity.Name).Reverse();
            return View();  //Oturum açan kullanıcının kendisine ait postlar tarihe göre sıralı
        }
        [Authorize]
        public ActionResult AddPost()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddPost(string title, string content)
        {
            ApplicationUser u = (from user in ent.Users where user.UserName == HttpContext.User.Identity.Name select user).FirstOrDefault();
            bool sonuc = false;
            Post p = new Post();
            p.PostTitle = title;
            p.PostContent = content; //Yeni postun Database Kaydı.
            p.UserPıcture = u.UserPicture;
            p.UserName = HttpContext.User.Identity.Name;
            try
            {
               if( repoP.Add(p))
                    sonuc = true;

            }
            catch (Exception)
            {

                throw;
            }           
            return Json(new { success = sonuc });
        }
        [Authorize]
        public ActionResult MyPost()
        {
            return View(); //Index sayfasının oluşturduğu ViewBag'ı görüntüleyen PartialView
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            bool sonuc = false;
            try
            {
                if (repoP.Delete(repoP.GetById(id)))
                    sonuc = true;
            }
            catch (Exception)
            {

                throw;
            }
            
            return Json(new { success = sonuc });
        }
        [Authorize]
        public ActionResult Edit(int id)
        {          
            return View(repoP.GetById(id));
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model)
        {
            Post p = (from post in ent.Posts where post.Id == model.Id select post).FirstOrDefault();
            p.PostTitle = model.PostTitle;
            p.PostContent = model.PostContent;
            if (string.IsNullOrEmpty(p.PostTitle) || string.IsNullOrEmpty(p.PostContent))
                return View(model);
            try
            {
                ent.SaveChanges();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

            return RedirectToAction("Index");
        }
    }
}