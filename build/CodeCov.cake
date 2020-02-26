#addin nuget:?package=Cake.Codecov&version=0.8.0
#tool nuget:?package=Codecov&version=1.10.0
#load "./Configurations.cake"
// Tasks
Task("CodeCov")
    .Does(() =>
{
    Codecov(
        CoverageFile,
        CodeCovToken
    );
});