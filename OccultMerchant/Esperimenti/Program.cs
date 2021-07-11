using System;

namespace Esperimenti
{
    class Program
    {
        static void Main(string[] args)
        {
            Guid a = Guid.Parse("93aa2119-b5af-48ed-97c0-a61c3f52d717");
            Guid b = Guid.Parse("93AA2119-B5AF-48ED-97C0-A61C3F52D717");
            Console.WriteLine(a.CompareTo(b) == 0);
            
        }
    }
}