
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5656

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["loanApi.csproj", "."]
RUN dotnet restore "./loanApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "loanApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "loanApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://+:5656
ENTRYPOINT ["dotnet", "loanApi.dll"]