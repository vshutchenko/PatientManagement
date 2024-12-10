FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PatientManagement.Api/PatientManagement.Api.csproj", "PatientManagement.Api/"]
COPY ["PatientManagement.DataAccess/PatientManagement.DataAccess.csproj", "PatientManagement.DataAccess/"]
COPY ["PatientManagement.Services/PatientManagement.Services.csproj", "PatientManagement.Services/"]
RUN dotnet restore "./PatientManagement.Api/PatientManagement.Api.csproj"
COPY . .
WORKDIR "/src/PatientManagement.Api"
RUN dotnet build "./PatientManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PatientManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PatientManagement.Api.dll"]