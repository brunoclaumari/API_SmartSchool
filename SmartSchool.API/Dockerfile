
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copia o arquivo "csproj" e restaura como layers distintos
COPY *.csproj ./
RUN dotnet restore

#Copia todas as coisas e então executa o build
COPY . ./
RUN dotnet publish -c Release -o out

#Faz o build da imagem do runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SmartSchool.API.dll"]