using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}