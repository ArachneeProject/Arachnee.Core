using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class ArachneeInnerCore_TestsProject : CSharpProject
    {
        public ArachneeInnerCore_TestsProject()
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
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Tests/InnerCore.Tests";
            conf.TargetPath = RootPath + @"\Outputs.Tests\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
			
			conf.AddPublicDependency<ArachneeInnerCore_Project>(target);
        }
    }	
}