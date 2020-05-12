using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using twitterWebUi.ViewModel;

namespace twitterWebUi.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public string imageUrl { get; set; }
        public string Email { get; set; }

        public List<Tweet> Tweets { get; set; }
        public List<Liked> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Retweet> Retweets { get; set; }
        public List<Follower> Followers { get; set; }

    }
}