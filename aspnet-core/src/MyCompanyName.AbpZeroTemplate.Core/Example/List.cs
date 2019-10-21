using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Example
{
	[Table("Lists")]
    public class List : Entity 
    {

		public virtual int CarID { get; set; }
		
		public virtual string name { get; set; }
		
		public virtual string detail { get; set; }
		
		public virtual string price { get; set; }
		

    }
}