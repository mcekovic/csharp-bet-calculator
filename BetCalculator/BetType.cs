using System;
using System.Collections.Generic;

namespace BetCalculator
{
    public abstract class BetType<T>
    {
        public abstract IEnumerator<IList<T>> Combinations(IList<T> items);
    }
}
