#FROM mono:latest
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /src
# copy published application files inside the image
COPY seed2/app /src

# configure web servers to bind to port 80 when present
#ENV ASPNETCORE_URLS http://localhost:80
    # Enable detection of running in a container
    #DOTNET_RUNNING_IN_CONTAINERS=true `
    # ignore first time expierence
    #DOTNET_SKIP_FIRST_TIME_EXPERIENCE="true"
#EXPOSE 80
# start the exe on container startup
#ENTRYPOINT ["mono", "Aquaservice.exe"]
ENTRYPOINT ["dotnet", "Aquaservice.dll"]