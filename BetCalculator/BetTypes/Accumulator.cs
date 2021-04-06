using System;
using System.Collections.Generic;

namespace BetCalculator.BetTypes
{
 
    public class Accumulator<T> : BetType<T>
    {
        public override IEnumerator<IList<T>> Combinations(IList<T> items) =>
            new List<IList<T>> {items}.GetEnumerator();
    }

    public class AccumulatorN<T> : Accumulator<T>
    {
        private readonly int _itemCount;

        public AccumulatorN(int itemCount)
        {
            if (itemCount < 1)
                throw new ArgumentException("itemCount must be greater than or equal to 1");
            _itemCount = itemCount;
        }

        public override IEnumerator<IList<T>> Combinations(IList<T> items)
        {
            if (items.Count != _itemCount)
                throw new ArgumentException("legs/groups size must be " + _itemCount);
            return base.Combinations(items);
        }
    }

    public class Single<T> : AccumulatorN<T>
    {
        public Single() : base(1) {}
    }
}
