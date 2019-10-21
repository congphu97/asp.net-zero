
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class CreateOrEditPersonDto : EntityDto<int?>
    {

		public string name { get; set; }
		
		
		public string age { get; set; }
		
		

    }
}