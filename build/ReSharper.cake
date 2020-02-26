#load "./Configurations.cake"
#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2019.3.1
// TASKS
Task("ResharperInspect")
   .Does(() =>
{
   InspectCode(
      SolutionName,
      new InspectCodeSettings
      {
         OutputFile = ResharperInspectXml,
         SolutionWideAnalysis = true
      });

   XmlTransform(ResharperInspectTransform, ResharperInspectXml, ResharperInspectHtml);
});