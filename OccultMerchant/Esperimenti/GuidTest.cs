using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace Esperimenti
{
    [Guid("9245fe4a-d402-451c-b9ed-9c1a04247482")]  
    public class GuidTest
    {
        public Guid test { get; set; }

        public GuidTest()
        {
            this.test = Guid.NewGuid();
        }

        public GuidTest(string str)
        {
            Guid tmp = new();
            Guid.TryParse(str,out tmp);
            this.test = tmp;
        }
    }
}