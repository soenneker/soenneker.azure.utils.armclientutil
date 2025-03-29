using Soenneker.Azure.Utils.ArmClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Azure.Utils.ArmClientUtil.Tests;

[Collection("Collection")]
public class ArmClientUtilTests : FixturedUnitTest
{
    private readonly IArmClientUtil _util;

    public ArmClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IArmClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
