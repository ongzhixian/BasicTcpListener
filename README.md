# BasicTcpListener

A basic .NET Core TCP listener console application use for simple deployments in Kubernetes. 


## dotnet CLI

dotnet CLI used to create this project:

```ps1: In C:\src\github.com\ongzhixian\BasicTcpListener
dotnet new sln -n BasicTcpListener
dotnet new console -n BasicTcpListener.ConsoleApp
dotnet sln .\BasicTcpListener.sln add .\BasicTcpListener.ConsoleApp\

dotnet add .\BasicTcpListener.ConsoleApp\ package Microsoft.Extensions.Configuration
dotnet add .\BasicTcpListener.ConsoleApp\ package Microsoft.Extensions.Configuration.Json

```

Other packages that we may want to include to expand on configuration options:
Microsoft.Extensions.Configuration.CommandLine
Microsoft.Extensions.Configuration.Binder
Microsoft.Extensions.Configuration.EnvironmentVariables 