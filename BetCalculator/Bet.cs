using System.Collections.Generic;
using BetCalculator.Rules;

namespace BetCalculator
{
    public class Bet
    {
        public BetType<BetLeg> BetType { get; init; }
        public decimal UnitStake { get; init; }
        public IList<BetLeg> Legs { get; init; }
        public BetRules Rules { get; init; } = BetRules.Default;
    }
}
