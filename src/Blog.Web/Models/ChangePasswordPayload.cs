namespace Blog.Web.Models
{
    public class ChangePasswordPayload
    {
        public string NewPassword { get; set; }

        public string CurrentPassword { get; set; }
    }
}