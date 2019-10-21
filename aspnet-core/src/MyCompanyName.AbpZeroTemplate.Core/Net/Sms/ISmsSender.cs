using System.Threading.Tasks;

namespace MyCompanyName.AbpZeroTemplate.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}