/*
 * Written by: Shay Friedman, @ironshay
 * Based on http://www.west-wind.com/weblog/posts/2010/Dec/27/Hosting-the-Razor-Engine-for-Templating-in-NonWeb-Applications
 * This code can be done using the RazorEngine nuget package as well.
 */

using System;
using System.Windows.Forms;
using System.Dynamic;

namespace Demo
{
	public partial class Form1 : Form
	{
		private RazorEngine Engine { get; set; }
		private TemplateBase CurrentTemplate { get; set; }

		public Form1()
		{
			InitializeComponent();
			Engine = new RazorEngine();			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			tbOutput.ResetText();

			CurrentTemplate = Engine.GenerateTemplateClass(tbTemplate.Text);
			propertyGridTemplate.SelectedObject = CurrentTemplate;
			
			button2.Enabled = true;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			tbOutput.Text = Engine.GetOutput(CurrentTemplate);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			dynamic context = new ExpandoObject();
			context.Name = tbName.Text;
			context.Message = tbMessage.Text;

			SurpriseClass.GenerateSurprise(context);
		}
	}
}
