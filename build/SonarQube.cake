#load "./Configurations.cake"
#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.6.0
#addin nuget:?package=Cake.Sonar&version=1.1.22
// TASKS
Task("SonarStart")
   .Does(() =>
{
   SonarBegin(new SonarBeginSettings
   {
      Login = SonarToken,
      Key = SonarProjectKey,
      Url = SonarUrl,
      Organization = SonarOrganization,
      Verbose = true,
      Exclusions = SonarExclusions,
      ArgumentCustomization = args =>
      {
         foreach (var argument in SonarArguments)
         {
             args.Append(argument);
         }
         return args;
      }
   });
});
Task("SonarEnd")
  .Does(() =>
{
   SonarEnd(new SonarEndSettings
   {
      Login = SonarToken
   });
});