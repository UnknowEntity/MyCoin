using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCompare
{
    public class Confirm
    {
        [JsonProperty("address")]
        public string Address { set; get; }
        [JsonProperty("amount")]
        public float Amount { set; get; }

    }

    public class GetConfirmRes
    {
        public Confirm[] Confirms { set; get; }
    }

    public class TransactionResponse
    {
        [JsonProperty("status")]
        public string Status { set; get; }
        [JsonProperty("id")]
        public string Id { set; get; }
    }

    public class InputJSON
    {
        [JsonProperty("address")]
        public string Address { set; get; }
        [JsonProperty("signature")]
        public string Signature { set; get; }
    }

    public class OutputJSON
    {
        [JsonProperty("amount")]
        public float Amount { set; get; }
        [JsonProperty("address")]
        public string Address { set; get; }
        [JsonProperty("publicKey")]
        public string PublicKey { set; get; }
    }

    public class TransactionJSON
    {
        [JsonProperty("id")]
        public string Id { set; get; }
        [JsonProperty("inputs")]
        public InputJSON[] Inputs { set; get; }
        [JsonProperty("outputs")]
        public OutputJSON[] Outputs { set; get; }
        [JsonProperty("type")]
        public string Type { set; get; }
        [JsonProperty("timestamp")]
        public Int64 Timestamp { set; get; }
    }

    class ResponseClass
    {
    }
}
