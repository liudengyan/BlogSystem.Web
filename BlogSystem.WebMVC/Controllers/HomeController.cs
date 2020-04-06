using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogSystem.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        BLL.ArticleManager articleManager = new BLL.ArticleManager();
        BLL.UserManager UserManager = new BLL.UserManager();
        // GET: Home

        //分页：使用webdiyer mvcpager这个包
        public ActionResult Index(int id=1)
        {
            return View(articleManager.GetAllArticle(id,3));
        }


        [HttpGet]
        public ActionResult Creat()
        {
            if (Session["username"]==null)
            {
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return View();
            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Creat(Models.CreatActricleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await articleManager.CreateAsync(model.Title, model.Content, Session["username"].ToString());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "发表文章失败");
                    ModelState.AddModelError("", e);
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Models.UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                if(await UserManager.LoginAsync(userLoginModel.Name, userLoginModel.Password))
                {
                    Session["username"] = userLoginModel.Name;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "登录失败");
                }
            }
            return View(userLoginModel);
        }

    }
}