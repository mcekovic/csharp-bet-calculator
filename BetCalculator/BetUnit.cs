using System.Collections.Generic;
using System.Linq;
using BetCalculator.Rules;
using static BetCalculator.BetState;
using static BetCalculator.Rules.EachWayType;

namespace BetCalculator
{
    public class BetUnit
    {
        private readonly decimal _unitStake;
        private readonly IList<BetLeg> _legs;
        private readonly BetRules _rules;

        public BetUnit(decimal unitStake, IList<BetLeg> legs, BetRules rules) 
        {
            _unitStake = unitStake;
            _legs = legs;
            _rules = rules;
        }

        public decimal UnitCount() => UnitCount(_rules.EachWayType);
        
        public decimal Stake() => UnitCount() * _unitStake;

        public decimal CurrentReturn()
        {
            var state = State();
            return state == Won || state == Void ? CalculatedReturn() : 0.0m;
        }

        public decimal MaxReturn() => State() != Lost ? CalculatedReturn() : 0.0m;

        private decimal CalculatedReturn() => Stake() * CumulativePrice();

        public decimal CumulativePrice() =>
            _rules.EachWayType != EachWay ? SimpleCumulativePrice() : EachWayCumulativePrice();

        private decimal SimpleCumulativePrice() =>
            _legs.Aggregate(1.0m, (current, leg)
                => current * leg.FactoredPrice(_rules.EachWayType)
            );

        private decimal EachWayCumulativePrice() =>
            _legs.Aggregate(EachWayAmounts.One, (current, leg)
                => current * new EachWayAmounts(leg.FactoredPrice(Win), leg.FactoredPrice(Place))
            ).Total / UnitCount(_rules.EachWayType);

        private static decimal UnitCount(EachWayType eachWayType) =>
            eachWayType switch
            {
                Win => 1.0m,
                Place => 1.0m,
                EachWay => 2.0m
            };

        public BetState State() =>
            _legs.Aggregate(Void, (state, leg) => BetStates.LegAggregate(state, leg.Status.State));
    }
}
