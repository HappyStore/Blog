﻿namespace Blog.Web.Models
{
    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }

        public string CurrentPassword { get; set; }
    }
}