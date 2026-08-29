[![](https://img.shields.io/nuget/v/soenneker.azure.utils.armclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.utils.armclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.utils.armclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.azure.utils.armclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.azure.utils.armclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.azure.utils.armclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.azure.utils.armclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.azure.utils.armclientutil/actions/workflows/codeql.yml)

# Soenneker.Azure.Utils.ArmClientUtil

Creates and caches an Azure Resource Manager `ArmClient` authenticated with a service-principal client secret.

## Installation

```bash
dotnet add package Soenneker.Azure.Utils.ArmClientUtil
```

## Configuration

```json
{
  "Azure": {
    "TenantId": "tenant-guid",
    "AppRegistration": {
      "Id": "application-client-id",
      "Secret": "client-secret"
    }
  }
}
```

Store the secret in Azure Key Vault, an environment variable, or another secret provider. Assign the service principal only the Azure RBAC roles its callers need.

## Registration and use

```csharp
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Soenneker.Azure.Utils.ArmClientUtil.Abstract;
using Soenneker.Azure.Utils.ArmClientUtil.Registrars;

builder.Services.AddArmClientUtilAsSingleton();

public sealed class SubscriptionReader(IArmClientUtil armClientUtil)
{
    public async Task<List<string>> GetSubscriptionNames(
        CancellationToken cancellationToken)
    {
        ArmClient client = await armClientUtil.Get(cancellationToken);
        var names = new List<string>();

        await foreach (SubscriptionResource subscription in
            client.GetSubscriptions().GetAllAsync(cancellationToken))
        {
            names.Add(subscription.Data.DisplayName);
        }

        return names;
    }
}
```

## Lifecycle and authentication behavior

- The `ArmClient` is created on first use and reused afterward.
- Missing tenant, client ID, or secret configuration fails initialization.
- Configuration and secret rotation do not alter an initialized client; replace the utility instance to use new credentials.
- Azure authorization is evaluated by ARM for each resource operation. Possessing a valid client secret does not grant access without the corresponding RBAC assignment.
- Let DI dispose the utility.

This package specifically uses `ClientSecretCredential`. Workloads that can use managed identity or workload identity should construct `ArmClient` with the appropriate `TokenCredential` instead of storing a client secret.
