FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Schmoli.ServiceTemplate/Schmoli.ServiceTemplate.csproj", "src/Schmoli.ServiceTemplate/"]
RUN dotnet restore "src/Schmoli.ServiceTemplate/Schmoli.ServiceTemplate.csproj"
COPY . .
WORKDIR "/src/src/Schmoli.ServiceTemplate"
RUN dotnet build "Schmoli.ServiceTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Schmoli.ServiceTemplate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Schmoli.ServiceTemplate.dll"]
