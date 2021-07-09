FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SecondaryLocation/SecondaryLocation.csproj", "SecondaryLocation/"]
RUN dotnet restore "SecondaryLocation/SecondaryLocation.csproj"
COPY . .
WORKDIR "/src/SecondaryLocation"
RUN dotnet build "SecondaryLocation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SecondaryLocation.csproj" -c Release -o /app/publish



FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SecondaryLocation.dll"]
