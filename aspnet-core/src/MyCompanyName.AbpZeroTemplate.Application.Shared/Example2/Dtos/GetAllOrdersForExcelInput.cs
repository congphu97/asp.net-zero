using Abp.Application.Services.Dto;
using System;

namespace MyCompanyName.AbpZeroTemplate.Example2.Dtos
{
    public class GetAllOrdersForExcelInput
    {
		public string Filter { get; set; }

		public string ProductIDFilter { get; set; }

		public string OrderDateFilter { get; set; }

		public string StatusFilter { get; set; }

		public string PaymentDateFilter { get; set; }

		public int? MaxAmountFilter { get; set; }
		public int? MinAmountFilter { get; set; }

		public long? MaxTotalVATFilter { get; set; }
		public long? MinTotalVATFilter { get; set; }

		public string UserFilter { get; set; }



    }
}