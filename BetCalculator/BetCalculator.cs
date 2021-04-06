namespace BetCalculator
{
    public static class BetCalculator
    {
        public static decimal Calculate(Bet bet)
        {
            var maxReturn = 0.0m;
            var combinations = bet.BetType.Combinations(bet.Legs);
            while (combinations.MoveNext())
            {
                var unit = new BetUnit(bet.UnitStake, combinations.Current, bet.Rules);
                maxReturn += unit.MaxReturn();
            }
            return maxReturn;
        }
    }
}
