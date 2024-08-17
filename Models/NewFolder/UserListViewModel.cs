namespace BiteBlogs.Models.NewFolder
{
    public class UserListViewModel
    {
        public List<User> UserList { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public bool AdminRoleCheckBox { get; set; }
    }
}
