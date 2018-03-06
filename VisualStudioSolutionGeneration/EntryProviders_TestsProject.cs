using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class EntryProviders_TestsProject : CSharpProject
    {
        public EntryProviders_TestsProject()
        {
            Name = "Arachnee.EntryProviders.Tests";
            SourceRootPath = "[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"[project.Name]";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests";
            conf.TargetPath = RootPath + @"\Outputs.Tests\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
			
			conf.AddPublicDependency<EntryProviders_Project>(target);
        }
    }	
}