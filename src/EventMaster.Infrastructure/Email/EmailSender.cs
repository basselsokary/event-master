using EventMaster.Application.Common.Interfaces.Email;

namespace EventMaster.Infrastructure.Email;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string body)
    {
        // TODO
        return Task.CompletedTask;
    }
}