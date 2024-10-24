﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traidr.Application.IServices
{
   public interface IEmailSendingService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
