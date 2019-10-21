
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class CreateOrEditCarDto : EntityDto<int?>
    {

		public string name { get; set; }
		
		
		public string detail { get; set; }
		
		
		public string price { get; set; }
		
		

    }
}