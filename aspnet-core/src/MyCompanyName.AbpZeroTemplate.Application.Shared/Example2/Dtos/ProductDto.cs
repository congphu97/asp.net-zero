
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class ProductDto : EntityDto
    {
		public string CategoryID { get; set; }

		public string Name { get; set; }



    }
}