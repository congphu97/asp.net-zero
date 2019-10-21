using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class GetAllPersonsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string nameFilter { get; set; }

		public string ageFilter { get; set; }



    }
}