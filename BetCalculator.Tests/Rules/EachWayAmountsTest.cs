using BetCalculator.Rules;
using FluentAssertions;
using Xunit;

namespace BetCalculator.Tests.Rules
{
    public class EachWayAmountsTest
    {
        [Fact]
        public void PlusTest()
        {
            (new EachWayAmounts(1.0m, 2.0m) + new EachWayAmounts(3.0m, 4.0m))
                .Should().Be(new EachWayAmounts(4.0m, 6.0m));
        }

        [Fact]
        public void TimesTest()
        {
            (new EachWayAmounts(1.0m, 2.0m) * new EachWayAmounts(3.0m, 4.0m))
                .Should().Be(new EachWayAmounts(3.0m, 8.0m));
        }

        [Fact]
        public void TotalTest()
        {
            new EachWayAmounts(4.0m, 2.0m).Total.Should().Be(6.0m);
            EachWayAmounts.Zero.Total.Should().Be(0.0m);
        }
    }
}