FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN mkdir /app/
WORKDIR /app/

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
RUN mkdir /work/
WORKDIR /work

COPY HelloDotnetsB.csproj /work/HelloDotnetsB.csproj
RUN dotnet restore
COPY . /work

RUN mkdir /out/

FROM build AS publish
RUN dotnet publish "HelloDotnetsB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDotnetsB.dll"]