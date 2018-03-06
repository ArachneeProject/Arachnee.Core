using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class EntryProviders_Project : CSharpProject
    {
        public EntryProviders_Project()
        {
            Name = "Arachnee.EntryProviders";
            SourceRootPath = "[project.SharpmakeCsPath]/../Core/EntryProviders";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"[project.Name]";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Core/EntryProviders";
            conf.TargetPath = RootPath + @"\Outputs\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("Newtonsoft.Json", "11.0.1");

			conf.AddPublicDependency<InnerCore_Project>(target);            
        }
    }	
}