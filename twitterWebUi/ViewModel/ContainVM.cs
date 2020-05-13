using System.Collections.Generic;
using twitterWebUi.Models;

namespace twitterWebUi.ViewModel
{
    public class ContainVM
    {
        public ContainVM()
        {
            CreateVM = new CreateVM();
            ListVM = new ListVM();
            UserListVM = new UserListVM();
            EditVM = new EditVM();
        }
        public CreateVM CreateVM { get; set; }
        public ListVM ListVM { get; set; }
        public UserListVM UserListVM { get; set; }
        public EditVM EditVM { get; set; }
    }
}