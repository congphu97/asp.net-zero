using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}