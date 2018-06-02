using System.IO;
using Sharpmake;

[module: Sharpmake.Include("InnerCore_Project.cs")]
[module: Sharpmake.Include("InnerCore_TestsProject.cs")]

[module: Sharpmake.Include("EntryProviders_Project.cs")]
[module: Sharpmake.Include("EntryProviders_TestsProject.cs")]

namespace SharpmakeGeneration
{
    // Represents the solution that will be generated.
    [Generate]
    class GeneratedSolution : CSharpSolution
    {
        public GeneratedSolution()
        {
            Name = "Arachnee.Core";
            AddTargets(GeneratedSolution.Target);
        }

        // Entry point
        [Main]
        public static void SharpmakeMain(Arguments sharpmakeArgs)
        {
            sharpmakeArgs.Generate<GeneratedSolution>();
        }

        [Configure]
        public void ConfigureAll(Solution.Configuration conf, Target target)
        {
            conf.SolutionFileName = "Arachnee.Core";
            conf.SolutionPath = Path.Combine("[solution.SharpmakeCsPath]", "..");

            // Add projects here
            conf.AddProject<InnerCore_Project>(target);
            conf.AddProject<TmdbProvider_Project>(target);
            conf.AddProject<LocalProvider_Project>(target);
			
			// Tests projects
            conf.AddProject<InnerCore_TestsProject>(target);
			conf.AddProject<TmdbProvider_TestsProject>(target);
			conf.AddProject<LocalProvider_TestsProject>(target);
			
        }

        public static Target Target
        {
            get
            {
                return new Target(

                    // 32/64 bits
                    Platform.win64 | Platform.win32,

                    // Visual Studio environment
                    DevEnv.vs2017,

                    // Configuration
                    Optimization.Debug | Optimization.Release,

                    // .NET Framework
                    framework: DotNetFramework.v4_5_2
                );
            }
        }
    }
}