FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

RUN mkdir /app/
WORKDIR /app/

FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
RUN mkdir /work/
WORKDIR /work

COPY ./HelloDotnets/HelloDotnets.csproj /work/HelloDotnets.csproj
RUN dotnet restore
COPY ./HelloDotnets /work

RUN mkdir /out/

FROM build AS publish
RUN dotnet publish "HelloDotnets.csproj" -c Release -o /app/publish
# RUN dotnet publish --no-restore --output /out/ --configuration Release

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloDotnets.dll"]