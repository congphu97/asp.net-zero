using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class GetCategoryForEditOutput
    {
		public CreateOrEditCategoryDto Category { get; set; }


    }
}