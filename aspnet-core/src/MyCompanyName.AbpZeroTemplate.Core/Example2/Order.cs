using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
	[Table("Orders")]
    public class Order : Entity 
    {

		public virtual string ProductID { get; set; }
		
		public virtual string OrderDate { get; set; }
		
		public virtual string Status { get; set; }
		
		public virtual string PaymentDate { get; set; }
		
		public virtual int Amount { get; set; }
		
		public virtual long TotalVAT { get; set; }
		
		public virtual string User { get; set; }
		

    }
}