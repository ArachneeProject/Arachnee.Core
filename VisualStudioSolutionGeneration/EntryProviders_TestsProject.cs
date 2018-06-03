using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class TmdbProviders_TestsProject : CSharpProject
    {
        public TmdbProviders_TestsProject()
        {
            Name = "Arachnee.TmdbProviders.Tests";
            SourceRootPath = "[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests/TmdbProviders.Tests";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"[project.Name]";
			conf.SolutionFolder = "Tests/EntryProviders.Tests";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests/TmdbProviders.Tests";
            conf.TargetPath = RootPath + @"\Outputs.Tests\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
			
			conf.AddPublicDependency<TmdbProviders_Project>(target);
        }
    }	
}