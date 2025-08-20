FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["SearchFight/SearchFight.csproj", "SearchFight/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "SearchFight/SearchFight.csproj"

COPY . .
WORKDIR "/src/SearchFight"

RUN dotnet build "SearchFight.csproj" -c Release -o /app/build
RUN dotnet publish "SearchFight.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80
ENTRYPOINT ["dotnet", "SearchFight.dll"]