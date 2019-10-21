using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class GetAllVariantsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string NameFilter { get; set; }

		public string ProductIDFilter { get; set; }

		public long? MaxPriceFilter { get; set; }
		public long? MinPriceFilter { get; set; }



    }
}