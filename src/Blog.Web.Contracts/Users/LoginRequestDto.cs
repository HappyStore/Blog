﻿namespace Blog.Web.Contracts.Users
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        
        public string Password { get; set; }
    }
}