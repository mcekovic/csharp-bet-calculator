using System.Collections.Generic;
using BetCalculator.BetTypes;
using BetCalculator.Rules;
using Xunit;
using FluentAssertions;

namespace BetCalculator.Tests
{
    public class BetCalculatorTest
    {
        [Fact]
        public void TestSingle()
        {
            var bet = new Bet { BetType = new Single<BetLeg>(), UnitStake = 10.0m, Legs = new List<BetLeg> {
                new() { Price = 2.0m }
            }};

            var maxReturn = BetCalculator.Calculate(bet);

            maxReturn.Should().Be(20.0m);
        }

        [Fact]
        public void TestAccumulator()
        {
            var bet = new Bet { BetType = new Accumulator<BetLeg>(), UnitStake = 5.0m, Legs = new List<BetLeg> {
                new() { Price = 2.0m },
                new() { Price = 3.0m }
            }};

            var maxReturn = BetCalculator.Calculate(bet);

            maxReturn.Should().Be(30.0m);
        }

        [Fact]
        public void TestPerms()
        {
            var bet = new Bet { BetType = new Perms<BetLeg>(2), UnitStake = 1.0m, Legs = new List<BetLeg> {
                new () { Price = 2.0m },
                new () { Price = 3.0m },
                new () { Price = 4.0m }
            }};

            var maxReturn = BetCalculator.Calculate(bet);

            maxReturn.Should().Be(26.0m);
        }
        
        [Fact]
        public void TestEachWaySingleBet() {
            var bet = new Bet { BetType = new Single<BetLeg>(), UnitStake = 10.0m, Legs = new List<BetLeg> {
                new () { Price = 2.0m, Status = LegStatus.OpenEW(0.5m)}
            }, Rules = new() { EachWayType = EachWayType.EachWay}};

            var maxReturn = BetCalculator.Calculate(bet);

            maxReturn.Should().Be(35.0m);
        }
    }
}
