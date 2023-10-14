#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TesteSebrae.WebApi/TesteSebrae.WebApi.csproj", "src/TesteSebrae.WebApi/"]
RUN dotnet restore "src/TesteSebrae.WebApi/TesteSebrae.WebApi.csproj"
COPY . .
WORKDIR "/src/src/TesteSebrae.WebApi"
RUN dotnet build "TesteSebrae.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteSebrae.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteSebrae.WebApi.dll"]