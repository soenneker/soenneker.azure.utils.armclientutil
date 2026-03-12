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
    ValueTask<ArmClient> Get(CancellationToken cancellationToken = default);
}
