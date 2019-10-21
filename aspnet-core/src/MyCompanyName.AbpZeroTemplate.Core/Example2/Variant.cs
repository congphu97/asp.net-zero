using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
	[Table("Variants")]
    public class Variant : Entity 
    {

		public virtual string Name { get; set; }
		
		public virtual string ProductID { get; set; }
		
		public virtual long Price { get; set; }
		

    }
}