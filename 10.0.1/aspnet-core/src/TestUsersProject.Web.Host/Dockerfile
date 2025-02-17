FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/TestUsersProject.Web.Host/TestUsersProject.Web.Host.csproj", "src/TestUsersProject.Web.Host/"]
COPY ["src/TestUsersProject.Web.Core/TestUsersProject.Web.Core.csproj", "src/TestUsersProject.Web.Core/"]
COPY ["src/TestUsersProject.Application/TestUsersProject.Application.csproj", "src/TestUsersProject.Application/"]
COPY ["src/TestUsersProject.Core/TestUsersProject.Core.csproj", "src/TestUsersProject.Core/"]
COPY ["src/TestUsersProject.EntityFrameworkCore/TestUsersProject.EntityFrameworkCore.csproj", "src/TestUsersProject.EntityFrameworkCore/"]
WORKDIR "/src/src/TestUsersProject.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/TestUsersProject.Web.Host", "src/TestUsersProject.Web.Host"]
COPY ["src/TestUsersProject.Web.Core", "src/TestUsersProject.Web.Core"]
COPY ["src/TestUsersProject.Application", "src/TestUsersProject.Application"]
COPY ["src/TestUsersProject.Core", "src/TestUsersProject.Core"]
COPY ["src/TestUsersProject.EntityFrameworkCore", "src/TestUsersProject.EntityFrameworkCore"]
WORKDIR "/src/src/TestUsersProject.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "TestUsersProject.Web.Host.dll"]
