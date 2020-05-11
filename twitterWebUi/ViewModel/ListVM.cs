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
        }
        public List<Tweet> Tweets { get; set; }
        public User User { get; set; }
    }
}