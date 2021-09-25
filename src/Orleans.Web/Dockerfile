#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Orleans.Web/Orleans.Web.csproj", "src/Orleans.Web/"]
RUN dotnet restore "src/Orleans.Web/Orleans.Web.csproj"
COPY . .
WORKDIR "/src/src/Orleans.Web"
RUN dotnet build "Orleans.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orleans.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orleans.Web.dll"]