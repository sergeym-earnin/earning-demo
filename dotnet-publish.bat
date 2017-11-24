set current_dir=%~dp0

cd %current_dir%\Earning.Demo
dotnet publish -c debug -o published

cd %current_dir%\Earning.Demo.Api
dotnet publish -c debug -o published

cd %current_dir%\Earning.Demo.Web
dotnet publish -c debug -o published

cd %current_dir%