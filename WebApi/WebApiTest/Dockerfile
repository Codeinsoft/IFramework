#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WebApi/WebApiTest/IFramework.WebApi.csproj", "WebApi/WebApiTest/"]
COPY ["WebApi/Core/IFramework.Api.Core.csproj", "WebApi/Core/"]
COPY ["Application/Core/IFramework.Application.Core.csproj", "Application/Core/"]
COPY ["Insfrastructure/Transversal/Mapper/Mapper/IFramework.Infrastructure.Transversal.Map.csproj", "Insfrastructure/Transversal/Mapper/Mapper/"]
COPY ["Insfrastructure/Transversal/Logger/Logger/IFramework.Infrastructure.Transversal.Logger.csproj", "Insfrastructure/Transversal/Logger/Logger/"]
COPY ["Domain/Core/IFramework.Domain.Core.csproj", "Domain/Core/"]
COPY ["Application/Contract/Core/IFramework.Application.Contract.Core.csproj", "Application/Contract/Core/"]
COPY ["Insfrastructure/Transversal/IoC/CastleWindsor/IoCResolver/IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCResolver.csproj", "Insfrastructure/Transversal/IoC/CastleWindsor/IoCResolver/"]
COPY ["Application/Application/IFramework.Application.csproj", "Application/Application/"]
COPY ["Application/Contract/Contract/IFramework.Application.Contract.csproj", "Application/Contract/Contract/"]
COPY ["Insfrastructure/Transversal/Utility/IFramework.Infrastructure.Utility.csproj", "Insfrastructure/Transversal/Utility/"]
COPY ["Insfrastructure/Transversal/Resource/IFramework.Infrastructure.Transversal.Resources.csproj", "Insfrastructure/Transversal/Resource/"]
COPY ["Insfrastructure/Constants/Exception/IFramework.Infrastructure.Constants.Exception.csproj", "Insfrastructure/Constants/Exception/"]
COPY ["Domain/Domain/IFramework.Domain.csproj", "Domain/Domain/"]
COPY ["Insfrastructure/Transversal/Mapper/AutoMapper/IFramework.Infrastructure.Transversal.Mapper.AutoMapper.csproj", "Insfrastructure/Transversal/Mapper/AutoMapper/"]
COPY ["Insfrastructure/Transversal/Logger/NLog/IFramework.Infra.Transversal.Log.NLog.csproj", "Insfrastructure/Transversal/Logger/NLog/"]
COPY ["Insfrastructure/Persistence/EFCore/IFramework.Infrastructure.Persistence.EFCore.csproj", "Insfrastructure/Persistence/EFCore/"]
COPY ["Insfrastructure/Persistence/UnitOfWork/IFramework.Infrastructure.Persistence.UnitOfWork.csproj", "Insfrastructure/Persistence/UnitOfWork/"]
COPY ["Insfrastructure/Transversal/IoC/CastleWindsor/IoCInstaller/IFramework.Infrastructure.Transversal.IoC.CastleWindsor.IoCInstaller.csproj", "Insfrastructure/Transversal/IoC/CastleWindsor/IoCInstaller/"]
COPY ["Insfrastructure/Transversal/Aspect/Exception/IFramework.Infrastructure.Transversal.Aspect.ExceptionAspect.csproj", "Insfrastructure/Transversal/Aspect/Exception/"]
COPY ["Insfrastructure/Persistence/Persistence/IFramework.Infrastructure.Persistence.csproj", "Insfrastructure/Persistence/Persistence/"]
COPY ["Insfrastructure/Persistence/FNibernate/IFramework.Infrastructure.Persistence.FNhibernate.csproj", "Insfrastructure/Persistence/FNibernate/"]
RUN dotnet restore "WebApi/WebApiTest/IFramework.WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi/WebApiTest"
RUN dotnet build "IFramework.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IFramework.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IFramework.WebApi.dll"]
