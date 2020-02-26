#load "./Configurations.cake";
#addin nuget:?package=Cake.Coverlet&version=2.3.4
#tool nuget:?package=Pickles.CommandLine&version=2.20.1
// TASKS
Task("CleanArtifacts")
   .Does(() =>
{
   DotNetCoreClean(
      SolutionName,
      new DotNetCoreCleanSettings
      {
         Configuration = configuration
      });
      
   if (DirectoryExists(ArtifactsDirectory))
   {
      DeleteDirectory(
         ArtifactsDirectory,
         new DeleteDirectorySettings
         {
            Force = true,
            Recursive = true
         });
   }
});
Task("Restore")
   .Does(() =>
{
   NuGetRestore(SolutionName);
});
Task("Build")
   .Does(() =>
{
   DotNetBuild(
      SolutionName,
      config =>
      {
         config.Configuration = configuration;
      });
});
Task("Test")
   .Does(() =>
{
   var testSettings = new DotNetCoreTestSettings
   {
      Configuration = configuration,
      NoBuild = true,
      NoRestore = true,
      Logger = $"trx;LogFileName={TestResult}"
   };
   var coverletSettings = new CoverletSettings
   {
      CollectCoverage = true,
      CoverletOutputFormat = CoverletOutputFormat.opencover | CoverletOutputFormat.cobertura,
      CoverletOutputDirectory = TestResultsDirectory,
      CoverletOutputName = TestResultsFile,
      OutputTransformer = (f, d) => $@"{d}/{f}"
   };
   DotNetCoreTest(
      SolutionName,
      testSettings,
      coverletSettings
   );
});
Task("LivingDocumentation")
   .Does(() =>
{
   StartProcess(
      PicklesExecutable,
      $"--feature-directory={FeaturesDirectory} " +
      $"--output-directory={ResultsDirectory} " +
      $"--link-results-file={ResultsDirectory}/{TestResult} " +
      "--test-results-format=vstest " +
      "--documentation-format=dhtml");
});