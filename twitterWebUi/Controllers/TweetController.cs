using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using twitterWebUi.Models;
using twitterWebUi.ViewModel;

namespace twitterWebUi.Controllers
{
    public class TweetController : Controller
    {
        TwitterContext db = new TwitterContext();

        public IActionResult Create()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Create(ContainVM twt)
        {
            User user = new User();
            Tweet tweet = new Tweet();
            Random rnd = new Random();
            int rndNumberLike = rnd.Next(1, 15);
            int rndNumberRetweet = rnd.Next(1,30);
            int rndNumberComment = rnd.Next(1,30);
            tweet.Description = twt.CreateVM.Tweet.Description;
            tweet.LikedNumber = rndNumberLike;
            tweet.RetweetNumber = rndNumberRetweet;
            tweet.CommentNumber = rndNumberComment;
            tweet.UserId = twt.CreateVM.User.Id;
            db.Tweets.Add(tweet);
            db.SaveChanges();
            return RedirectToAction("Index", "Account", new { Id = tweet.UserId });
        }


        public IActionResult SetLiked(bool liked, int userid, int tweetid)
        {
            if (liked == false)
            {
                Liked like = new Liked();
                Tweet tweet = db.Tweets.Where(x => x.Id == tweetid).FirstOrDefault();
                User user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                int likenumber = tweet.LikedNumber;
                likenumber = likenumber + 1;
                tweet.LikedNumber = likenumber;
                like.TweetId = tweet.Id;
                like.UserId = user.Id;
                like.User = user;
                like.Tweet = tweet;
                db.Likes.Add(like);
                db.SaveChanges();

                return Json(new { result = tweet.LikedNumber });
            }
            else
            {
                Liked like = db.Likes.Where(x => x.TweetId == tweetid && x.UserId == userid).FirstOrDefault();
                Tweet tweet = db.Tweets.Where(x => x.Id == tweetid).FirstOrDefault();
                int likenumber = tweet.LikedNumber;
                likenumber = likenumber - 1;
                tweet.LikedNumber = likenumber;
                db.Likes.Remove(like);
                db.Tweets.Update(tweet);
                db.SaveChanges();

                return Json(new { result = tweet.LikedNumber });

            }
        }

        public IActionResult GetLiked(int[] ids, int uid)
        {

            List<int> likeTweetids = db.Likes.Where(x => x.UserId == uid && ids.Contains(x.TweetId)).Select(x => x.TweetId).ToList();

            return Json(new { result = likeTweetids });
        }

        public IActionResult GetRetweet(int[] ids, int uid)
        {

            List<int> reTweetids = db.Retweets.Where(x => x.UserId == uid && ids.Contains(x.TweetId)).Select(x => x.TweetId).ToList();

            return Json(new { result = reTweetids });
        }

        public IActionResult SetRetweet(bool retweet, int userid, int tweetid)
        {
            if (retweet == false)
            {
                Retweet rtwt = new Retweet();
                Tweet tweet = db.Tweets.Where(x => x.Id == tweetid).FirstOrDefault();
                User user = db.Users.Where(x => x.Id == userid).FirstOrDefault();
                int retweetnumber = tweet.RetweetNumber;
                retweetnumber = retweetnumber + 1;
                tweet.RetweetNumber = retweetnumber;
                rtwt.TweetId = tweet.Id;
                rtwt.UserId = user.Id;
                rtwt.User = user;
                rtwt.Tweet = tweet;
                db.Retweets.Add(rtwt);
                db.SaveChanges();

                return Json(new { result = tweet.RetweetNumber });
            }
            else
            {
                Retweet rtwt= db.Retweets.Where(x => x.TweetId == tweetid && x.UserId == userid).FirstOrDefault();
                Tweet tweet = db.Tweets.Where(x => x.Id == tweetid).FirstOrDefault();
                int retweetnumber = tweet.RetweetNumber;
                retweetnumber = retweetnumber - 1;
                tweet.RetweetNumber = retweetnumber;
                db.Retweets.Remove(rtwt);
                db.Tweets.Update(tweet);
                db.SaveChanges();

                return Json(new { result = tweet.RetweetNumber });

            }
        }

        public IActionResult GetDescription(int tweetid,int userid){
            Tweet tweet = db.Tweets.Where(x=>x.Id==tweetid).FirstOrDefault();
            User user = db.Users.Where(x=>x.Id==userid).FirstOrDefault();

            var Result = new{ result = tweet.Description, username = user.Username};

            return Json(new{Result});
        }

        public IActionResult CreateComment(int tweetid,int userid,string commentdescription){
            Comment comment = new Comment();
            comment.Description=commentdescription;
            comment.TweetId=tweetid;
            comment.UserId = userid;
            db.Comments.Add(comment);
            db.SaveChanges();
            return Json(new{});
        }

        public IActionResult AddFollower(int followerid,int uid){

            Follower follower =new Follower();
            follower.FollowerId= followerid;
            follower.UserId = uid;
            db.Followers.Add(follower);
            db.SaveChanges();

            return Json(new{ result=follower.FollowerId});
        }
    }
}