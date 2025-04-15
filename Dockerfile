# Use the official .NET image from Microsoft
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Apiproject/Apiproject.csproj", "Apiproject/"]
RUN dotnet restore "Apiproject/Apiproject.csproj"
COPY . .
WORKDIR "/src/Apiproject"
RUN dotnet build "Apiproject.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Apiproject.csproj" -c Release -o /app/publish

# Copy the published app from the build image and set the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Apiproject.dll"]
