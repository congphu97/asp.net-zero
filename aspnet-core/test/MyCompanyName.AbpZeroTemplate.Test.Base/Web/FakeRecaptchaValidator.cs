using System.Threading.Tasks;
using MyCompanyName.AbpZeroTemplate.Security.Recaptcha;

namespace MyCompanyName.AbpZeroTemplate.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
