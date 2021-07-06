using System.Collections.Generic;
using Warehouse.items;

namespace Warehouse.Database
{
    public class Filter
    {
        public List<string> names { get; set; }

        public Filter()
        {
            this.names = new List<string>();
        }

        public Filter(List<string> names)
        {
            this.names = names;
        }

        /// <summary>
        /// otini la lista dei paramatrei da usare per il confronto nells get e i valori da modificare nella set
        /// </summary>
        /// <returns></returns>
        public string setSting()
        {
            string res = "";
            foreach (string name in this.names)
            {
                if (res != "")
                {
                    res += ", ";
                }

                res += $"{name}=@{name}";
            }

            return res;
        }

        public string getString()
        {
            string res = "";
            foreach (string name in this.names)
            {
                if (res != "")
                {
                    res += ", ";
                }

                res += $"{name}=%@{name}%";
            }

            return res;
        }
       
        
    }
    
        
        
        
    
}