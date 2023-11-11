FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["fruitful.api/fruitful.api.csproj", "fruitful.api/"]
RUN dotnet restore "fruitful.api/fruitful.api.csproj"
COPY . .
WORKDIR "/src/fruitful.api"
RUN dotnet build "fruitful.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fruitful.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fruitful.api.dll"]
