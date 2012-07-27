using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataAnnotationsExtensions;

namespace Clean.Models
{
	public class Dog
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		[Email]
		public string Email { get; set; }
	}
}