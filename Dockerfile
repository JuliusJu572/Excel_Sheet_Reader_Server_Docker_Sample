#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ExcelReader.csproj", "."]
RUN dotnet restore "./ExcelReader.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ExcelReader.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExcelReader.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ��¶Ӧ�ó���ʹ�õĶ˿�
EXPOSE 5000

# ����Ӧ�ó���
ENTRYPOINT ["dotnet", "ExcelReader.dll"]