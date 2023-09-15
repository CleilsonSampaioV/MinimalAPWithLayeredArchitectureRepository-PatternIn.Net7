#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/MyTrainingSheet.Api/MyTrainingSheet.Api.csproj", "src/MyTrainingSheet.Api/"]
COPY ["src/MyTrainingSheet.Business/MyTrainingSheet.Business.csproj", "src/MyTrainingSheet.Business/"]
COPY ["src/MyTrainingSheet.Domain/MyTrainingSheet.Domain.csproj", "src/MyTrainingSheet.Domain/"]
COPY ["src/MyTrainingSheet.Infra/MyTrainingSheet.Infra.csproj", "src/MyTrainingSheet.Infra/"]
RUN dotnet restore "src/MyTrainingSheet.Api/MyTrainingSheet.Api.csproj"
COPY . .
WORKDIR "/src/src/MyTrainingSheet.Api"
RUN dotnet build "MyTrainingSheet.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyTrainingSheet.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyTrainingSheet.Api.dll"]