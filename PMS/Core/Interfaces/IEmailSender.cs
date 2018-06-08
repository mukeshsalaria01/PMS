﻿using System.Threading.Tasks;

namespace PMS.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
