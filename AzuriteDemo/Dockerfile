#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AzuriteDemo/AzuriteDemo.csproj", "AzuriteDemo/"]
RUN dotnet restore "AzuriteDemo/AzuriteDemo.csproj"
COPY . .
WORKDIR "/src/AzuriteDemo"
RUN dotnet build "AzuriteDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzuriteDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzuriteDemo.dll"]