using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MassAssignmentDemo.Models
{
	[Bind(Exclude="IsAdmin")]
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public bool IsAdmin { get; set; }
	}
}