ARG DOTNET_TAG=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_TAG} AS build
WORKDIR /src
COPY KinesisApi/*.csproj .
RUN dotnet restore --use-current-runtime
COPY KinesisApi/ .
RUN dotnet publish --use-current-runtime -o /app

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_TAG}
WORKDIR /app
EXPOSE 80
COPY --from=build /app .
ENTRYPOINT ["dotnet", "KinesisApi.dll"]
