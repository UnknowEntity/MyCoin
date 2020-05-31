using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCompare
{
    class Input
    {
        public string Address { get; set; }
        public int[] Signature { get; set; }

        public Input(string Address, int[] Signature)
        {
            this.Address = Address;
            this.Signature = Signature;
        }


    }
}
