namespace Blog.Handlers.Users
{
    public class ChangePasswordResult
    {
        public ChangePasswordStatus Status { get; set; }

        public string? StatusMessage { get; set; }
    }
}