using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCompare
{
    class Confirm
    {
        public string Address { set; get; }
        public float Amount { set; get; }

    }

    class GetConfirmRes
    {
        public Confirm[] Confirms { set; get; }
    }

    class ResponseClass
    {
    }
}
