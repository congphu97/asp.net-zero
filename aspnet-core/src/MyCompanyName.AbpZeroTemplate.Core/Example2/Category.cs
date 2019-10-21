using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Example2
{
	[Table("Categories")]
    public class Category : Entity 
    {

		public virtual string Name { get; set; }
		

    }
}