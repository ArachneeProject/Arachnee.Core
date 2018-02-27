using System.IO;
using Sharpmake;

[module: Sharpmake.Include("ArachneeInnerCore_Project.cs")]
[module: Sharpmake.Include("ArachneeInnerCore_TestsProject.cs")]

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
            conf.AddProject<ArachneeInnerCore_Project>(target);
			
			// Tests projects
            conf.AddProject<ArachneeInnerCore_TestsProject>(target);
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
                    framework: DotNetFramework.v4_5
                );
            }
        }
    }
}