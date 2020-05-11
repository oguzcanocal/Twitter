using System.Collections.Generic;
using twitterWebUi.Models;

namespace twitterWebUi.ViewModel
{
    public class UserListVM
    {
        public UserListVM()
        {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }   
    }
}