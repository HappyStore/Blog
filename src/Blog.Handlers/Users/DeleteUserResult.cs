namespace Blog.Handlers.Users
{
    public class DeleteUserResult
    {
        public DeleteUserStatus Status { get; set; }
        public string? StatusMessage { get; set; }
    }
}