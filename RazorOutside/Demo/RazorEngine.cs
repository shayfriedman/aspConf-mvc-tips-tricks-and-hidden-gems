/*
 * Written by: Shay Friedman, @ironshay
 * Based on http://www.west-wind.com/weblog/posts/2010/Dec/27/Hosting-the-Razor-Engine-for-Templating-in-NonWeb-Applications
 * This code can be done using the RazorEngine nuget package as well.
 */

using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Web.Razor;
using Microsoft.CSharp;

namespace Demo
{
	public class RazorEngine
	{
		private RazorTemplateEngine _engine;

		public RazorEngine()
		{
			// Set up the hosting environment			
			SetupHost();
		}

		public TemplateBase GenerateTemplateClass(string template)
		{
			// Generate code for the template
			GeneratorResults razorResult = GenerateCode(template);

			// Compile the generated code into an assembly
			string outputAssemblyName = CompileCode(razorResult);

			return CreateTemplateInstance(outputAssemblyName);
		}

		public string GetOutput(TemplateBase template)
		{
			template.Execute();
			string result = template.Buffer.ToString();
			template.Buffer.Clear();

			return result;
		}

		private void SetupHost()
		{
			// a. Use the C# language (you could detect this based on the file extension if you want to)
			var host = new RazorEngineHost(new CSharpRazorCodeLanguage());

			// b. Set the base class
			host.DefaultBaseClass = typeof (TemplateBase).FullName;

			// c. Set the output namespace and type name
			host.DefaultNamespace = "RazorOutput";
			host.DefaultClassName = "Template";

			// d. Add default imports
			host.NamespaceImports.Add("System");
			host.NamespaceImports.Add("System.IO");
			host.NamespaceImports.Add("System.Dynamic");

			_engine = new RazorTemplateEngine(host);
		}

		private TemplateBase CreateTemplateInstance(string outputAssemblyName)
		{
			Assembly asm = Assembly.LoadFrom(outputAssemblyName);

			// Get the template type
			Type typ = asm.GetType("RazorOutput.Template");

			return Activator.CreateInstance(typ) as TemplateBase;
		}

		private string CompileCode(GeneratorResults razorResult)
		{
			var codeProvider = new CSharpCodeProvider();

			string outputAssemblyName = String.Format("Temp_{0}.dll", Guid.NewGuid().ToString("N"));
			codeProvider.CompileAssemblyFromDom(
				new CompilerParameters(new[]
				                       	{
				                       		typeof (Form1).Assembly.CodeBase.Replace("file:///", "").Replace("/", "\\"),
				                       		"System.Core.dll",
				                       		"Microsoft.CSharp.dll"
				                       	}, outputAssemblyName),
				razorResult.GeneratedCode);

			return outputAssemblyName;
		}

		private GeneratorResults GenerateCode(string template)
		{
			GeneratorResults razorResult = null;
			using (TextReader rdr = new StringReader(template))
			{
				razorResult = _engine.GenerateCode(rdr);
			}

			return razorResult;
		}
	}
}