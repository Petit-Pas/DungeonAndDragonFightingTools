using DnDToolsLibrary.Memory;

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
}
