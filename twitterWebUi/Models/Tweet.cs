using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace twitterWebUi.Models
{
    public class Tweet
    {   
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }
        public string Description { get; set; }
        public int LikedNumber { get; set; }
        public int RetweetNumber { get; set; }
        public int CommentNumber { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
        public List<Liked> Likes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Retweet> Retweets { get; set; }

    }
}