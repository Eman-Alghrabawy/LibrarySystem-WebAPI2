FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LibrarySystem/LibrarySystem.csproj", "LibrarySystem/"]
RUN dotnet restore "LibrarySystem/LibrarySystem.csproj"

COPY . .
WORKDIR "/src/LibrarySystem"
RUN dotnet publish "LibrarySystem.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LibrarySystem.dll"]
