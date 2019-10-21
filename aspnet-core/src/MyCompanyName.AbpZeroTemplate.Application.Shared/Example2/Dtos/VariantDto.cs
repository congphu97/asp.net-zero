
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class VariantDto : EntityDto
    {
		public string Name { get; set; }

		public string ProductID { get; set; }

		public long Price { get; set; }



    }
}