FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80
COPY ./published .
#ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Earning.Demo.Web.dll"]
