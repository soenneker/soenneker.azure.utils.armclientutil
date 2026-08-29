using Azure.ResourceManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Azure.Utils.ArmClientUtil.Abstract;

/// <summary>
/// A .NET thread-safe singleton for ArmClient, the Azure Resource Manager
/// </summary>
public interface IArmClientUtil : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Returns the configured arm Client used by the arm client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested arm Client.</returns>
    ValueTask<ArmClient> Get(CancellationToken cancellationToken = default);
}
