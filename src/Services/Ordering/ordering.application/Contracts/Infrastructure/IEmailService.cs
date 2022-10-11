
using ordering.application.Models;
using System.Threading.Tasks;

namespace ordering.application.Contracts.Infrastructure
{
   public  interface IEmailService
    {

     
            Task<bool> SendEmail(Email email);
       
    }
}
