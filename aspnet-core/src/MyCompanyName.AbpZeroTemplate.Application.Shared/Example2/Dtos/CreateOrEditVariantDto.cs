
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class CreateOrEditVariantDto : EntityDto<int?>
    {

		public string Name { get; set; }
		
		
		public string ProductID { get; set; }
		
		
		public long Price { get; set; }
		
		

    }
}