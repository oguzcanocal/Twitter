using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace twitterWebUi.Models
{
    public class Follower
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]  
        public int FollowerId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}