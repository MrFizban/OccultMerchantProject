using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using OccultMerchant.items;

namespace Esperimenti
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Price pr = new Price(20,CoinType.SilverCoin);
            string str = pr.ToString();

            string[] lista = str.Split( ':');
            Price tmp = new Price(int.Parse(lista[0].Substring(1)), (CoinType) int.Parse(lista[1].Remove(lista[1].Length-1)));

        }
    }
}