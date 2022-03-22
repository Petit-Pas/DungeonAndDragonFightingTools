namespace SandBox
{
    
    class Program
    {
        
        public class A
        {
            public A(string name)
            {
                Name = name;
            }
            public string Name { get; }
        }

        public class B : A
        {
            public B(string name) : base(name) { }
        }

        static void Main(string[] args)
        {
            List<A> list = new List<A>();

            list.Add(new A("1A"));
            list.Add(new A("2A"));
            list.Add(new A("3A"));
            list.Add(new A("4A"));
            list.Add(new B("1B"));
            list.Add(new B("2B"));
            list.Add(new B("3B"));
            list.Add(new B("4B"));
            list.Select(x => x.Name == "1A");
            ;
        }
    }
}
