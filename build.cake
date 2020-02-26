#load "./build/Configurations.cake"
#load "./build/BuildAndTest.cake"
#load "./build/SonarQube.cake"
#load "./build/Resharper.cake"
#load "./build/Publish.cake"
#load "./build/CodeCov.cake"
// ARGUMENTS
var target = Argument("target", "BuildPublish");
configuration = Argument("configuration", "Release");
// TASKS
Task("QuickBuild")
   .IsDependentOn("CleanArtifacts")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("Test");
Task("BuildPublish")
   .IsDependentOn("CleanArtifacts")
   .IsDependentOn("Restore")
   .IsDependentOn("Build")
   .IsDependentOn("Test")
   .IsDependentOn("PublishApi");
Task("CI")
   .IsDependentOn("CleanArtifacts")
   .IsDependentOn("Restore")
   .IsDependentOn("SonarStart")
   .IsDependentOn("Build")
   .IsDependentOn("Test")
   .IsDependentOn("SonarEnd")
   .IsDependentOn("CodeCov")
   .IsDependentOn("LivingDocumentation")
   .IsDependentOn("ResharperInspect")
   .IsDependentOn("PublishApi");
RunTarget(target);
