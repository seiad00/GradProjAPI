# 빌드 환경
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app



COPY . .
RUN dotnet restore "Unity2D/Unity2D.csproj"
RUN dotnet publish "Unity2D/Unity2D.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 실행 환경
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "Unity2D.dll"]