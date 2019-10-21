using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
	[Table("Products")]
    public class Product : Entity 
    {

		public virtual string CategoryID { get; set; }
		
		public virtual string Name { get; set; }
		

    }
}