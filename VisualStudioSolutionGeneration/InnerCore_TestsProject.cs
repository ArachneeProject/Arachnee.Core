using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class InnerCore_TestsProject : CSharpProject
    {
        public InnerCore_TestsProject()
        {
            Name = "Arachnee.InnerCore.Tests";
            SourceRootPath = "[project.SharpmakeCsPath]/../Tests/InnerCore.Tests";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"Arachnee.InnerCore.Tests";
			conf.SolutionFolder = "Tests/InnerCore.Tests";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Tests/InnerCore.Tests";
            conf.TargetPath = RootPath + @"\Outputs.Tests\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
			
			conf.AddPublicDependency<InnerCore_Project>(target);
        }
    }	
}