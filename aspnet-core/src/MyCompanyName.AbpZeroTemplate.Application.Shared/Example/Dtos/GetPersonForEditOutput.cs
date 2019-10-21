using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class GetPersonForEditOutput
    {
		public CreateOrEditPersonDto Person { get; set; }


    }
}