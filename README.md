# Implode

A small library which extends asp.net core healthtchecks to run, and then crash the application on startup in the case of a unhealthy health check.

## Why? 

You prefer a newly deployed application to fail on deployment rather than fully startup and have to check the healthcheck endpoint.

## How to use

```csharp
    public void ConfigureServices(IServiceCollection services)
    {
	      //Other configure services code
        services.AddImplodeOnStartupForUnhealthyHealthChecks
    }
```
## How to install

```dotnet add package Implode```
