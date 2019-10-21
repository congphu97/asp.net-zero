using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace MyCompanyName.AbpZeroTemplate.Example
{
	[Table("Persons")]
    public class Person : Entity 
    {

		public virtual string name { get; set; }
		
		public virtual string age { get; set; }
		

    }
}