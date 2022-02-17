#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ScheduleGenerator/ScheduleGenerator.csproj", "ScheduleGenerator/"]
RUN dotnet restore "ScheduleGenerator/ScheduleGenerator.csproj"
COPY . .
WORKDIR "/src/ScheduleGenerator"
RUN dotnet build "ScheduleGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduleGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleGenerator.dll"]