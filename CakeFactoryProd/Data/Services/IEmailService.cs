using CakeFactoryProd.Models;
using System.Threading.Tasks;
using SendGrid;

namespace CakeFactoryProd.Data.Services
{
    public interface IEmailService
    {
        Task<Response> SendSingleEmail(ComposeEmailModel payload);
    }

}
