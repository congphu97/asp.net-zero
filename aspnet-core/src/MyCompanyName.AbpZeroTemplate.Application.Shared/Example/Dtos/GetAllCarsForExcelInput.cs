using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class GetAllCarsForExcelInput
    {
		public string Filter { get; set; }

		public string nameFilter { get; set; }

		public string detailFilter { get; set; }

		public string priceFilter { get; set; }



    }
}