FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80
COPY ./published .

ENTRYPOINT ["dotnet", "Earning.Demo.Api.dll"]
