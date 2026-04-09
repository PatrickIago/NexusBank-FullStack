# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os csproj separadamente (cache eficiente)
COPY User.Domain/User.Domain.csproj User.Domain/
COPY User.Application/User.Application.csproj User.Application/
COPY User.Infra/User.Infra.csproj User.Infra/
COPY User.Presentation/User.Presentation.csproj User.Presentation/

RUN dotnet restore User.Presentation/User.Presentation.csproj

# Copia o resto do código
COPY . .

# Publica a API
RUN dotnet publish User.Presentation/User.Presentation.csproj -c Release -o /app/publish

# =========================
# RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "User.Presentation.dll"]