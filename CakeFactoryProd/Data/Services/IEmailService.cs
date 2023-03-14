using CakeFactoryProd.Models;
using SendGrid;

namespace CakeFactoryProd.Data.Services
{
    public interface IEmailService
    {
        Task<Response> SendSingleEmail(ComposeEmailModel payload);
    }
}
