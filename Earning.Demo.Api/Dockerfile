FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 8080
COPY .\bin\Release\PublishOutput .
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "Earning.Demo.Api.dll"]
