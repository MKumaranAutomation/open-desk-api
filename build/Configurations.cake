// Variables
private string configuration = "Release";
const string SolutionName = "./OpenDesk.sln";
const string ArtifactsDirectory = "./artifacts";
const string ProjectToPublish = "./src/Api.Http/Api.Http.csproj";
const string TestResultsDirectory = "./test-results";
const string TestResultsFile = "results";
private readonly string TestResult = $"{TestResultsFile}.trx";
const string SonarProjectKey = "spicycoder_open-desk-api";
const string SonarOrganization = "spicycoder";
const string SonarToken = "b0f2b05ab57b4c765f010b0601adcb21118c6892";
const string SonarUrl = "https://sonarcloud.io";
const string SonarExclusions = "**/*.css,**/*.html";
const string AnalysisDirectory = "./analysis";
private readonly static string CoverageFile = $"{TestResultsDirectory}/results.opencover.xml";
private readonly static string CodeCovToken = "e108d274-1e3e-4aaf-8eb4-1937ba1f12c5";
private readonly static string ResharperInspectXml = $"{AnalysisDirectory}/inspect.xml";
private readonly static string ResharperInspectTransform = $"{AnalysisDirectory}/inspect.xsl";
private readonly static string ResharperInspectHtml = $"{AnalysisDirectory}/index.html";
private readonly static string[] SonarArguments = new string[]
{
   "/d:sonar.cs.opencover.reportsPaths=\"**.opencover.xml\""
};
const string PicklesExecutable = "./tools/Pickles.CommandLine.2.20.1/tools/pickles.exe";
const string TestProject = "./tests/Api.Http.IntegrationTests/Api.Http.IntegrationTests.csproj";
private readonly static string FeaturesDirectory = $"./tests/Api.Http.IntegrationTests/Features";
private readonly static string ResultsDirectory = $"./tests/Api.Http.IntegrationTests/TestResults";