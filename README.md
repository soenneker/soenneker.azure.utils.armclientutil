[![](https://img.shields.io/nuget/v/soenneker.azure.utils.armclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.utils.armclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.utils.armclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.utils.armclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.utils.armclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.utils.armclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.utils.armclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.azure.utils.armclientutil/actions/workflows/codeql.yml)

# Soenneker.Azure.Utils.ArmClientUtil

A .NET thread-safe singleton for ArmClient, the Azure Resource Manager.

## Install

```bash
dotnet add package Soenneker.Azure.Utils.ArmClientUtil
```

## Quick start

```csharp
using Soenneker.Azure.Utils.ArmClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddArmClientUtilAsSingleton();
```

Adds `IArmClientUtil` as a singleton service.

## What you get

- `IArmClientUtil` — A .NET thread-safe singleton for ArmClient, the Azure Resource Manager.
- `ArmClientUtilRegistrar` — A .NET thread-safe singleton for ArmClient, the Azure Resource Manager.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `ArmClientUtilRegistrar.AddArmClientUtilAsSingleton(services)` | Adds `IArmClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `ArmClientUtilRegistrar.AddArmClientUtilAsScoped(services)` | Adds `IArmClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
