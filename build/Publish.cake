#load "./Configurations.cake"
// TASKS
Task("PublishApi")
   .Does(() =>
{
   DotNetCorePublish(
      ProjectToPublish,
      new DotNetCorePublishSettings
      {
         Configuration = configuration,
         NoBuild = true,
         NoRestore = true,
         OutputDirectory = ArtifactsDirectory
      });
});