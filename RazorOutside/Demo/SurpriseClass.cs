/*
 * Written by: Shay Friedman, @ironshay
 * Based on http://www.west-wind.com/weblog/posts/2010/Dec/27/Hosting-the-Razor-Engine-for-Templating-in-NonWeb-Applications
 * This code can be done using the RazorEngine nuget package as well.
 */

using System;
using System.IO;
using Application = Microsoft.Office.Interop.Word.Application;
using Word = Microsoft.Office.Interop.Word;
namespace Demo
{
	public class SurpriseClass
	{
		public static void GenerateSurprise(dynamic context)
		{
			string xml = File.ReadAllText("Demo.xml");
		
			RazorEngine engine = new RazorEngine();
			TemplateBase template = engine.GenerateTemplateClass(xml);
			template.Context = context;

			string output = engine.GetOutput(template);
			
			File.WriteAllText("DemoOutput.xml", output);

			Word.Application app = new Application();
			app.Visible = true;
			// Open the document that was chosen by the dialog
			Word.Document doc = app.Documents.Open(Path.Combine(Environment.CurrentDirectory, "DemoOutput.xml"));
			// Activate the document so it shows up in front
			doc.Activate();			
		}

		
	}
}
