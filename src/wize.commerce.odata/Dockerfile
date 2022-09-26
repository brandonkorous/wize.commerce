FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ./ ./

RUN dotnet restore "src/wize.commerce.odata/wize.commerce.odata.csproj"
COPY . .
WORKDIR "src/wize.commerce.odata"
RUN dotnet build "wize.commerce.odata.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wize.commerce.odata.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wize.commerce.odata.dll"]