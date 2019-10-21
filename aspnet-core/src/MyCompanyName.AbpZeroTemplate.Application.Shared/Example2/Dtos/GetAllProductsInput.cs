using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class GetAllProductsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string CategoryIDFilter { get; set; }

		public string NameFilter { get; set; }



    }
}