FROM microsoft/aspnetcore:2.0
WORKDIR /app
EXPOSE 80
COPY . .
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "Earning.Demo.Api.dll"]
