#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR /src
COPY . .
RUN dotnet restore "Catalog.API.csproj"
RUN dotnet publish "Catalog.API.csproj" -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=publish /src/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "Catalog.API.dll"]