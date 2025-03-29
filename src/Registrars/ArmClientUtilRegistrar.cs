using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Azure.Utils.ArmClientUtil.Abstract;

namespace Soenneker.Azure.Utils.ArmClientUtil.Registrars;

/// <summary>
/// A .NET thread-safe singleton for ArmClient, the Azure Resource Manager
/// </summary>
public static class ArmClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IArmClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddArmClientUtilAsSingleton(this IServiceCollection services)
    {
        services.TryAddSingleton<IArmClientUtil, ArmClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IArmClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddArmClientUtilAsScoped(this IServiceCollection services)
    {
        services.TryAddScoped<IArmClientUtil, ArmClientUtil>();

        return services;
    }
}
