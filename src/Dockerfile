FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY ["InvestmentMngmt.API/InvestmentMngmt.API.csproj", "InvestmentMngmt.API/"]
COPY ["InvestmentMngmt.Application/InvestmentMngmt.Application.csproj", "InvestmentMngmt.Application/"]
COPY ["InvestmentMngmt.Domain/InvestmentMngmt.Domain.csproj", "InvestmentMngmt.Domain/"]
RUN dotnet restore "InvestmentMngmt.API/InvestmentMngmt.API.csproj"
COPY . .
WORKDIR "/src/InvestmentMngmt.API"
RUN dotnet build "InvestmentMngmt.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestmentMngmt.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvestmentMngmt.API.dll"]