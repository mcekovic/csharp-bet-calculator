namespace BetCalculator.Rules
{
    public readonly struct EachWayAmounts
    {
        public static readonly EachWayAmounts Zero = new();
        public static readonly EachWayAmounts One = new(1.0m, 1.0m);
        
        private readonly decimal _win;
        private readonly decimal _place;

        public decimal Win => _win;

        public decimal Place => _place;

        public decimal Total => _win + _place;

        public EachWayAmounts(decimal win, decimal place)
        {
            _win = win;
            _place = place;
        }
        
        public static EachWayAmounts operator +(EachWayAmounts a, EachWayAmounts b) => 
            new(a._win + b._win, a._place + b._place);
        
        public static EachWayAmounts operator *(EachWayAmounts a, EachWayAmounts b) =>
            new(a._win * b._win, a._place * b._place);
    }
}