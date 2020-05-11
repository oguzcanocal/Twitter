using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace twitterWebUi.Models
{
    public class Liked
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }

    }   
}