using System;
using OccultMerchant.items;

namespace Esperimenti
{
    class Program
    {
        static void Main(string[] args)
        {
            Price pr = new Price(20,CoinType.SilverCoin);
            string str = pr.ToString();
            Console.WriteLine(str);
            string[] lista = str.Split('[', ']', ':');
            Console.WriteLine(lista);
            foreach (string s in lista)
            {
                Console.WriteLine(s);
            }
        }
    }
}