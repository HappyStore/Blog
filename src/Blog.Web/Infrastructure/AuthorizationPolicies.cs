namespace Blog.Web.Infrastructure
{
    public static class AuthorizationPolicies
    {
        public const string SuperAdmin = "superAdmin";
        public const string Administrator = "admin";
        public const string Moderator = "moderator";
    }
}