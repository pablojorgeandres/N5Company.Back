#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["N5Company/N5Company.csproj", "N5Company/"]
RUN dotnet restore "N5Company/N5Company.csproj"
COPY . .
WORKDIR "/src/N5Company"
RUN dotnet build "N5Company.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5Company.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5Company.dll"]