FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MovieRank/MovieRank.csproj", "MovieRank/"]
RUN dotnet restore "MovieRank/MovieRank.csproj"
COPY . .
WORKDIR "/src/MovieRank"
RUN dotnet build "MovieRank.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MovieRank.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieRank.dll"]
