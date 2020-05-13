using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
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
            int i = rnd.Next(1, 3);
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
            HttpContext.Session.SetInt32("SessionUser", Id);

            List<User> users = new List<User>();
            users = db.Users.ToList();
            users.Remove(user);
            users = users.Take(3).ToList();
            TempData["followList"] = users;

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

                    HttpContext.Session.SetString("SesionUser", JsonConvert.SerializeObject(User));


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

        public IActionResult Profile()
        {
            var userid = HttpContext.Session.GetInt32("SessionUser");
            User user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
            List<Follower> followers = db.Followers.Where(x => x.UserId == userid).ToList();
            List<User> users = new List<User>();
            foreach (var item in followers)
            {
                User user1 = new User();
                user1 = db.Users.Where(x => x.Id == item.FollowerId).FirstOrDefault();
                users.Add(user1);
            }

            List<Follower> following = db.Followers.Where(x => x.FollowerId == userid).ToList();
            List<User> usersfollowing = new List<User>();
            foreach (var item in following)
            {
                User user1 = new User();
                user1 = db.Users.Where(x => x.Id == item.UserId).FirstOrDefault();
                usersfollowing.Add(user1);
            }

            ListVM tweets = new ListVM();
            tweets.Tweets = db.Tweets.Where(x => x.UserId == userid).ToList();
            tweets.User = user;
            tweets.Users=users;
            tweets.FollowUsers=usersfollowing;
            tweets.User=user;
            return View(tweets);
        }

        public IActionResult ProfileEdit(int id){
            User user = db.Users.Where(x=>x.Id==id).FirstOrDefault();

            ContainVM updUser = new ContainVM();
            updUser.EditVM.Id=user.Id;
            updUser.EditVM.Username=user.Username;
            updUser.EditVM.Email=user.Email;
            updUser.EditVM.Password=user.Password;
            updUser.EditVM.imageUrl=user.imageUrl;
            return View(updUser);
        }

        [HttpPost]
        public IActionResult ProfileEdit(ContainVM user){
            User existinguser = db.Users.Where(x=>x.Id==user.EditVM.Id).FirstOrDefault();
            existinguser.Username=user.EditVM.Username;
            existinguser.Password=user.EditVM.Password;
            existinguser.imageUrl=user.EditVM.imageUrl;
            existinguser.Email=user.EditVM.Email;
            existinguser.Id=user.EditVM.Id;
            db.Users.Update(existinguser);
            db.SaveChanges();

            return View("Login");
        }

    }
}