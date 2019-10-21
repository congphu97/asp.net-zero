
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example.Dtos
{
    public class ListDto : EntityDto
    {
		public int CarID { get; set; }

		public string name { get; set; }

		public string detail { get; set; }

		public string price { get; set; }



    }
}