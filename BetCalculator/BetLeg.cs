using BetCalculator.Rules;

namespace BetCalculator
{
    public class BetLeg
    {
        public decimal Price { get; init; }
        public LegStatus Status { get; init; } = LegStatus.Open;
        
        public decimal FactoredPrice(EachWayType eachWayType) =>
            Status.FactoredPrice(Price, eachWayType);
    }
}