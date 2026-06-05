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
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<ArmClient> Get(CancellationToken cancellationToken = default);
}
