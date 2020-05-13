using System.Collections.Generic;
using twitterWebUi.Models;

namespace twitterWebUi.ViewModel
{
    public class ListVM
    {
        public ListVM()
        {
            User = new User();
            Tweets = new List<Tweet>();
            Users = new List<User>();
            FollowUsers = new List<User>();
        }
        public List<Tweet> Tweets { get; set; }
        public User User { get; set; }
        public List<User> Users { get; set; }
        public List<User> FollowUsers { get; set; }

    }
}