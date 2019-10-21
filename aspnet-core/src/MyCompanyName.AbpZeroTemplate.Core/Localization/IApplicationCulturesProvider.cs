using System.Globalization;

namespace MyCompanyName.AbpZeroTemplate.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}