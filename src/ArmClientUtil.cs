using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.ResourceManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Soenneker.Azure.Utils.ArmClientUtil.Abstract;
using Soenneker.Extensions.Configuration;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Azure.Utils.ArmClientUtil;

public class ArmClientUtil : IArmClientUtil
{
    private readonly AsyncSingleton<ArmClient> _client;

    public ArmClientUtil(IConfiguration configuration, ILogger<ArmClientUtil> logger)
    {
        _client = new AsyncSingleton<ArmClient>(() =>
        {
            var tenantId = configuration.GetValueStrict<string>("Azure:TenantId");
            var appRegistrationId = configuration.GetValueStrict<string>("Azure:AppRegistration:Id");
            var appRegistrationSecret = configuration.GetValueStrict<string>("Azure:AppRegistration:Secret");

            logger.LogDebug("Initializing Azure ArmClient...");

            var armCredentials = new ClientSecretCredential(tenantId, appRegistrationId, appRegistrationSecret);

            return new ArmClient(armCredentials);
        });
    }

    public ValueTask<ArmClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _client.DisposeAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _client.Dispose();
    }
}