
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class CreateOrEditProductDto : EntityDto<int?>
    {

		public string CategoryID { get; set; }
		
		
		public string Name { get; set; }
		
		

    }
}