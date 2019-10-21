
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class CreateOrEditOrderDto : EntityDto<int?>
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