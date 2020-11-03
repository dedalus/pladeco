FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base

RUN apt-get update && \   
    apt-get install -y gnupg  libgconf-2-4 wget && \
    wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - && \
    sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' && \
    apt-get update && \
    apt-get install -y google-chrome-unstable --no-install-recommends

ENV chrome:launchOptions:args --no-sandbox

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Pladeco.Web/Pladeco.Web.csproj", "Pladeco.Web/"]
COPY ["Model/Model.csproj", "Model/"]
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