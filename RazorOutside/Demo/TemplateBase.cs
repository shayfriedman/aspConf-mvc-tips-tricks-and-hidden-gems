/*
 * Written by: Shay Friedman, @ironshay
 * Based on http://www.west-wind.com/weblog/posts/2010/Dec/27/Hosting-the-Razor-Engine-for-Templating-in-NonWeb-Applications
 * This code can be done using the RazorEngine nuget package as well.
 */

using System.ComponentModel;
using System.IO;
using System.Text;

namespace Demo
{
	public abstract class TemplateBase
	{
		[Browsable(false)]
		public StringBuilder Buffer { get; set; }

		[Browsable(false)]
		public StringWriter Writer { get; set; }

		[Browsable(false)]
		public dynamic Context { get; set; }

		public TemplateBase()
		{
			Buffer = new StringBuilder();
			Writer = new StringWriter(Buffer);
		}

		public abstract void Execute();

		// Writes the results of expressions like: "@foo.Bar"
		public virtual void Write(object value)
		{
			// Don't need to do anything special
			// Razor for ASP.Net does HTML encoding here.
			WriteLiteral(value);
		}

		// Writes literals like markup: "<p>Foo</p>"
		public virtual void WriteLiteral(object value)
		{
			Buffer.Append(value);
		}

		public virtual void Write()
		{}
	}
}
