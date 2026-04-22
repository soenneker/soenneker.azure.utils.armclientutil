using Soenneker.Azure.Utils.ArmClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Azure.Utils.ArmClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class ArmClientUtilTests : HostedUnitTest
{
    private readonly IArmClientUtil _util;

    public ArmClientUtilTests(Host host) : base(host)
    {
        _util = Resolve<IArmClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
