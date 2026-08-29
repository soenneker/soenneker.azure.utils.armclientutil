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

/// <inheritdoc cref="IArmClientUtil"/>
public sealed class ArmClientUtil : IArmClientUtil
{
    private readonly AsyncSingleton<ArmClient> _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ArmClientUtil> _logger;

    public ArmClientUtil(IConfiguration configuration, ILogger<ArmClientUtil> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _client = new AsyncSingleton<ArmClient>(CreateClient);
    }

    private ArmClient CreateClient()
    {
        var tenantId = _configuration.GetValueStrict<string>("Azure:TenantId");
        var appRegistrationId = _configuration.GetValueStrict<string>("Azure:AppRegistration:Id");
        var appRegistrationSecret = _configuration.GetValueStrict<string>("Azure:AppRegistration:Secret");

        _logger.LogDebug("Initializing Azure ArmClient...");

        var armCredentials = new ClientSecretCredential(tenantId, appRegistrationId, appRegistrationSecret);

        return new ArmClient(armCredentials);
    }

    public ValueTask<ArmClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }
}