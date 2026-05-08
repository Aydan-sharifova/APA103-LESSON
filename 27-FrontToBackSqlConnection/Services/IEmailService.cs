namespace _27_FrontToBackSqlConnection.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
    void SendMail();
}
