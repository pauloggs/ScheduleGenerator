FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore
COPY ./ ./
RUN dotnet build "ScheduleGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduleGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleGenerator.dll"]