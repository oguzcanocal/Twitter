using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using twitterWebUi.Models;
using twitterWebUi.ViewModel;

namespace twitterWebUi.Controllers
{
    public class AccountController : Controller
    {

        TwitterContext db = new TwitterContext();

        public IActionResult Index(int Id)
        {
            var twt = db.Tweets.Where(x => x.UserId == Id).ToList();
            User user = db.Users.Where(x => x.Id == Id).FirstOrDefault();
            TempData["userid"] = user.Id;
            Random rnd = new Random();
            int i = rnd.Next(1,3);
            ContainVM model = new ContainVM();
            model.ListVM.Tweets = twt;
            model.ListVM.User.Id = Id;
            model.ListVM.User.Username = user.Username;
            model.ListVM.User.imageUrl = user.imageUrl;
            model.ListVM.User.Password = user.Password;
            model.ListVM.User.Email = user.Email;
            model.ListVM.User.CreateDate = user.CreateDate;
            model.CreateVM.User.Id = Id;
            model.CreateVM.Tweet.UserId = Id;
            
            List<User> users = new List<User>();
            users = db.Users.ToList();
            users.Remove(user);
            users= users.Take(3).ToList();
            TempData["followList"]=users;

            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                User user2 = db.Users.Where(x => x.Email == loginVM.Email && x.Password == loginVM.Password).FirstOrDefault();
                if (user2 == null)
                {
                    TempData["error"] = "Girdiğin e-posta ve şifre kayıtlarımızla eşleşmedi. Lütfen doğru girdiğinden emin ol ve tekrar dene.";
                    return View();
                }
                else
                {
                    ContainVM User = new ContainVM();
                    User.CreateVM.User.Id = user2.Id;
                    User.CreateVM.User.Username = user2.Username;
                    User.CreateVM.User.imageUrl = user2.imageUrl;
                    User.CreateVM.User.Password = user2.Password;
                    User.CreateVM.User.Tweets = user2.Tweets;
                    User.CreateVM.User.Email = user2.Email;
                    User.CreateVM.User.CreateDate = user2.CreateDate;


                    return RedirectToAction("Index", "Account", new { Id = user2.Id });

                }

            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                var User = db.Users.Where(x => x.Email == register.Email).FirstOrDefault();
                if (User == null)
                {
                    ContainVM model = new ContainVM();
                    model.CreateVM.User.Username = register.Username;
                    model.CreateVM.User.Password = register.Password;
                    model.CreateVM.User.Email = register.Email;
                    model.CreateVM.User.CreateDate = DateTime.Now;
                    db.Users.Add(model.CreateVM.User);
                    db.SaveChanges();
                    return View("Index", model);
                }
                else
                {
                    TempData["error"] = "E-posta adresi başka bir hesaba aittir";
                }

            }

            return View();
        }



        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult Profile(){
            return View();
        }

    }
}