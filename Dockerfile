FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NotaFiscalFaturamento.API/NotaFiscalFaturamento.API.csproj", "NotaFiscalFaturamento.API/"]
COPY ["NotaFiscalFaturamento.Application/NotaFiscalFaturamento.Application.csproj", "NotaFiscalFaturamento.Application/"]
COPY ["NotaFiscalFaturamento.CrossCutting/NotaFiscalFaturamento.CrossCutting.csproj", "NotaFiscalFaturamento.CrossCutting/"]
COPY ["NotaFiscalFaturamento.Domain/NotaFiscalFaturamento.Domain.csproj", "NotaFiscalFaturamento.Domain/"]
COPY ["NotaFiscalFaturamento.Infrastructure/NotaFiscalFaturamento.Infrastructure.csproj", "NotaFiscalFaturamento.Infrastructure/"]
RUN dotnet restore "NotaFiscalFaturamento.API/NotaFiscalFaturamento.API.csproj"
COPY . .
WORKDIR "/src/NotaFiscalFaturamento.API"
RUN dotnet build "NotaFiscalFaturamento.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NotaFiscalFaturamento.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotaFiscalFaturamento.API.dll"]
