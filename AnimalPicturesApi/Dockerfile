FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy everything, restore, build and publish
COPY . ./
RUN dotnet publish AnimalPicturesApi.csproj -c Release -o _publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/_publish .
ENTRYPOINT ["dotnet", "AnimalPicturesApi.dll"]