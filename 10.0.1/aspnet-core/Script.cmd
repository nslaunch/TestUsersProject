:: Runs unit tests from specific solution using .net version specified in .csproj
ECHO OFF
CLS
TITLE Batch script for running unit tests
ECHO .NET version is:
dotnet --version
:: restore packages
dotnet restore TestUsersProject.sln
:: build the solution 
dotnet build TestUsersProject.sln --no-restore
:: run the tests and generate report
:: code coverage tool install
:: dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet test "test\TestUsersProject.Web.Tests\TestUsersProject.Web.Tests.csproj" -p:CollectCoverage=true -p:CoverletOutput="TestResults/" -p:CoverletOutputFormat=cobertura --verbosity normal --logger "html;logfilename=RunResults.html" --results-directory "TestResults"
reportgenerator -reports:"test\TestUsersProject.Web.Tests\TestResults\coverage.cobertura.xml" -targetdir:"TestResults/CoverageResults" -reporttypes:Html
@echo off