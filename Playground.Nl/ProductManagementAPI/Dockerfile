FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy necessary files and restore as distinct layer
COPY ProductManagementAPI.API/ProductManagementAPI.API.csproj ./ProductManagementAPI.API/
COPY ProductManagementAPI.Database/ProductManagementAPI.Database.csproj ./ProductManagementAPI.Database/
COPY ProductManagementAPI.Services/ProductManagementAPI.Services.csproj ./ProductManagementAPI.Services/

#WORKDIR ./CustomerManagementAPI.Nl
RUN dotnet restore ProductManagementAPI.API/

# Copy everything else and build
COPY . ./
#RUN dotnet publish -c Release -o out
RUN ls
RUN dotnet publish ProductManagementAPI.API/ --no-restore -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY --from=build-env /app/out .

# Expose ports
EXPOSE 5300/tcp
ENV ASPNETCORE_URLS http://*:5300
HEALTHCHECK --interval=30s --timeout=3s --retries=1 CMD curl --silent --fail http://localhost:5300/hc || exit 1

# Start
ENTRYPOINT ["dotnet", "Playground.Nl.ProductManagementAPI.API.dll"]