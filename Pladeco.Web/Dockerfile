FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Pladeco.Web/Pladeco.Web.csproj", "Pladeco.Web/"]
COPY ["Pladeco.Domain/Pladeco.Domain.csproj", "Pladeco.Domain/"]
RUN dotnet restore "Pladeco.Web/Pladeco.Web.csproj"
COPY . .
WORKDIR "/src/Pladeco.Web"
RUN dotnet build "Pladeco.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pladeco.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pladeco.Web.dll"]