#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FoodDelivery/FoodDelivery.csproj", "FoodDelivery/"]
COPY ["FoodDelivery.BL/FoodDelivery.BL.csproj", "FoodDelivery.BL/"]
COPY ["FoodDelivery.DAL.Infrastructure/FoodDelivery.DAL.Infrastructure.csproj", "FoodDelivery.DAL.Infrastructure/"]
COPY ["FoodDelivery.DAL/FoodDelivery.DAL.EFCore.csproj", "FoodDelivery.DAL/"]
COPY ["FoodDelivery.Shared/FoodDelivery.Shared.csproj", "FoodDelivery.Shared/"]
RUN dotnet restore "FoodDelivery/FoodDelivery.csproj"
COPY . .
WORKDIR "/src/FoodDelivery"
RUN dotnet build "FoodDelivery.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodDelivery.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8000
ENV ASPNETCORE_URLS=http://+:8000
ENTRYPOINT ["dotnet", "FoodDelivery.dll"]