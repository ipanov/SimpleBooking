using System;
using System.Net.Mail;

namespace SimpleBookingSystem.Business.Emailer
{
    public interface ISmtpClient :IDisposable
    {
        void Send(MailMessage mailMessage);
    }
}
