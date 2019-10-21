
using System;
using Abp.Application.Services.Dto;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class OrderDto : EntityDto
    {
		public string ProductID { get; set; }

		public string OrderDate { get; set; }

		public string Status { get; set; }

		public string PaymentDate { get; set; }

		public int Amount { get; set; }

		public long TotalVAT { get; set; }

		public string User { get; set; }



    }
}