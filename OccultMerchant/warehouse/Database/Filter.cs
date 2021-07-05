using System.Collections.Generic;
using warehouse.items;

namespace warehouse.Controllers
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

        public string setString()
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
        
    }
    
        
        
        
    
}