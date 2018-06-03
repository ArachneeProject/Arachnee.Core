using System.IO;
using Sharpmake;

namespace SharpmakeGeneration
{
    // Represents the project that will be generated.
    [Generate]
    public class TmdbProviders_Project : CSharpProject
    {
        public TmdbProviders_Project()
        {
            Name = "Arachnee.TmdbProviders";
            SourceRootPath = "[project.SharpmakeCsPath]/../Core/EntryProviders/TmdbProviders";
            RootPath = "[project.SharpmakeCsPath]/../";
            AddTargets(GeneratedSolution.Target);
        }
        
        [Configure]
        public void ConfigureAll(Project.Configuration conf, Target target)
        {
            conf.Output = Configuration.OutputType.DotNetClassLibrary;
            
            conf.ProjectFileName = @"[project.Name]";
			conf.SolutionFolder = "Core/EntryProviders";
            conf.ProjectPath = @"[project.SharpmakeCsPath]/../Core/EntryProviders/TmdbProviders";
            conf.TargetPath = RootPath + @"\Outputs\[project.Name]";
            
            conf.ReferencesByName.Add("System");
			conf.ReferencesByNuGetPackage.Add("TMDbLib", "1.2.0-alpha");
			conf.ReferencesByNuGetPackage.Add("Newtonsoft.Json", "9.0.1");
			conf.ReferencesByNuGetPackage.Add("RestSharp", "105.2.3");

			conf.AddPublicDependency<InnerCore_Project>(target);            
        }
    }
}