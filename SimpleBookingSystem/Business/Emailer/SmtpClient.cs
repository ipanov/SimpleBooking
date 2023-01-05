using System.Net.Mail;
using System;

namespace SimpleBookingSystem.Business.Emailer
{
    public class SmtpClientWrapper : ISmtpClient
    {
        private bool disposed;
        //private readonly SmtpClient smtpClient;

        public SmtpClientWrapper(string host, int port)
        {
            //smtpClient = new SmtpClient(host, port);
        }

        ~SmtpClientWrapper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //smtpClient?.Dispose();
                }
                disposed = true;
            }
        }

        protected void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(SmtpClientWrapper));
            }
        }

        public void Send(MailMessage mailMessage)
        {
            CheckDisposed();
            //smtpClient.Send(mailMessage);
            Console.WriteLine(mailMessage.Body);
        }
    }
}

