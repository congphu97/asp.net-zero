using Abp.Auditing;
using MyCompanyName.AbpZeroTemplate.Configuration.Dto;

namespace MyCompanyName.AbpZeroTemplate.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}