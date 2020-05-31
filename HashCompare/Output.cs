using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCompare
{
    class Output
    {
        public string Address { get; set; }
        public float Amount { get; set; }
        public int[] PublicKey { get; set; }

        public Output(string Address, int[] PublicKey, float Amount)
        {
            this.Address = Address;
            this.PublicKey = PublicKey;
            this.Amount = Amount;
        }
    }
}
