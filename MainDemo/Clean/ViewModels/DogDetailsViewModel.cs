using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clean.ViewModels
{
	public class DogDetailsViewModel
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string EmailColor
		{
			get
			{
				if (Email.Contains("dogland.com"))
				{
					return "green";
				}
				return "black";
			}
		}
	}
}