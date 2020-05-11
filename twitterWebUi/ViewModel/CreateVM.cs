using twitterWebUi.Models;

namespace twitterWebUi.ViewModel
{
    public class CreateVM
    {
        public CreateVM()
        {
            User = new User();
            Tweet = new Tweet();
        }
        public Tweet Tweet { get; set; }
        public User User { get; set; }
    }
}