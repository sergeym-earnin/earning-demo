FROM microsoft/dotnet
WORKDIR /app

# copy and build everything else
COPY . .
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "Earning.Demo.dll"]
