using System;
using System.Collections.Generic;

namespace BetCalculator.BetTypes
{
 
    public class Perms<T> : BetType<T> 
    {
        private readonly int _combinationSize;

        public Perms(int combinationSize)
        {
            if (combinationSize < 1)
                throw new ArgumentException("combinationSize must be greater than or equal to 1");
            _combinationSize = combinationSize;
        }

        public override IEnumerator<IList<T>> Combinations(IList<T> items)
        {
            if (items.Count < _combinationSize)
                throw new ArgumentException("legs/groups size must be greater than or equal to " + _combinationSize);
            return new Combinations<T>(items, _combinationSize);
        }
    }
}
