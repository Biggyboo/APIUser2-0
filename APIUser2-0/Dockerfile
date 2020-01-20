FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["APIUser2-0/APIUser2-0.csproj", "APIUser2-0/"]
RUN dotnet restore "APIUser2-0/APIUser2-0.csproj"
COPY . .
WORKDIR "/src/APIUser2-0"
RUN dotnet build "APIUser2-0.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "APIUser2-0.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "APIUser2-0.dll"]