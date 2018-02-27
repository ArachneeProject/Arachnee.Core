using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class ArachneeInnerCore_Project : CSharpProject
    {
        public ArachneeInnerCore_Project()
        {
            Name = "Arachnee.InnerCore";
            SourceRootPath = "[project.SharpmakeCsPath]/../Core/InnerCore";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"Arachnee.InnerCore";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Core/InnerCore";
            conf.TargetPath = RootPath + @"\Outputs\[project.Name]";
            
            conf.ReferencesByName.Add("System");

            // conf.ReferencesByNuGetPackage.Add("NUnit", "3.9.0");
            // conf.AddPrivateDependency<InteropLibrary>(target);
        }
    }	
}