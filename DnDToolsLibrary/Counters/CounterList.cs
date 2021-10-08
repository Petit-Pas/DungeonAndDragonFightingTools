using DnDToolsLibrary.Memory;
using System;
using System.Linq;

namespace DnDToolsLibrary.Counters
{
    public class CounterList : GenericList<Counter>
    {
        public CounterList() : base()
        {
        }

        void init_copy(CounterList to_copy)
        {
        }

        public CounterList(CounterList to_copy) : base(to_copy)
        {
            init_copy(to_copy);
        }

        public override object Clone()
        {
            return new CounterList(this);
        }
    }

    public static class CounterListExtensions
    {
        public static bool IsEquivalentTo(this CounterList list, CounterList otherList)
        {
            if (list.Count != otherList.Count)
                return false;

            foreach (Tuple<Counter, Counter> counters in list.Zip<Counter, Counter, Tuple<Counter, Counter>>(otherList, (x, y) => new Tuple<Counter, Counter>(x, y) ))
            {
                if (!counters.Item1.IsEquivalentTo(counters.Item2))
                    return false;
            }
            return true;
        }
    }
}
