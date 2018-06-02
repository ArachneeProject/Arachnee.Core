using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class TmdbProvider_TestsProject : CSharpProject
    {
        public TmdbProvider_TestsProject()
        {
            Name = "Arachnee.TmdbProvider.Tests";
            SourceRootPath = "[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests/TmdbProvider.Tests";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"[project.Name]";
			conf.SolutionFolder = "Tests/EntryProviders.Tests";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests/TmdbProvider.Tests";
            conf.TargetPath = RootPath + @"\Outputs.Tests\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
			
			conf.AddPublicDependency<TmdbProvider_Project>(target);
        }
    }	
	
	// Represents the project that will be generated.
    [Generate]
    public class LocalProvider_TestsProject : CSharpProject
    {
        public LocalProvider_TestsProject()
        {
            Name = "Arachnee.LocalProvider.Tests";
            SourceRootPath = "[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests/LocalProvider.Tests";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"[project.Name]";
			conf.SolutionFolder = "Tests/EntryProviders.Tests";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Tests/EntryProviders.Tests/LocalProvider.Tests";
            conf.TargetPath = RootPath + @"\Outputs.Tests\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
			
			conf.AddPublicDependency<LocalProvider_Project>(target);
        }
    }	
}