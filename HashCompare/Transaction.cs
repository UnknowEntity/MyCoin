using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCompare
{
    class Transaction
    {
        public List<Input> Inputs { set; get; }
        public List<Output> Outputs { set; get; }
        public string Type { set; get; }

        /// <summary>
        /// Normal transaction. If return invalid then the transaction is not possible
        /// </summary>
        public Transaction(List<Output> outputs)
        {
            float total = 0;
            foreach(Output output in outputs)
            {
                total += output.Amount;
            }

            float[] change = new float[] { 0};
            Span<float> changeSpan = change.AsSpan();
            List<Input> inputs = XmlManager.GetTotal(total, change);

            XmlManager.SetSpend(inputs);

            if (change[0]==0)
            {
                Inputs = inputs;
                Outputs = outputs;
                Type = "normal";
            }
            else
            {
                byte[] privateKey = CryptoCurrency.Hash32Byte();
                byte[] publicKey = CryptoCurrency.GetPublicKey(privateKey);
                string address = CryptoCurrency.Sha1(publicKey);
                Output changeOutput = new Output(address,CryptoCurrency.ByteArrayToIntArray(publicKey),change[0]);
                Type = "change";
                XmlManager.NewKey(CryptoCurrency.ByteArrayToString(privateKey), CryptoCurrency.ByteArrayToString(publicKey), address, "change");
                outputs.Add(changeOutput);
                Inputs = inputs;
                Outputs = outputs;
                Type = "normal";
            }
        }

        /// <summary>
        /// First transaction amount given is 100 coin
        /// </summary>
        public Transaction()
        {
            byte[] privateKey = CryptoCurrency.Hash32Byte();
            byte[] publicKey = CryptoCurrency.GetPublicKey(privateKey);
            string address = CryptoCurrency.Sha1(publicKey);
            Inputs = null;
            Outputs = new List<Output>()
            {
                new Output(address,CryptoCurrency.ByteArrayToIntArray(publicKey),100)
            };
            Type = "first";
            XmlManager.NewKey(CryptoCurrency.ByteArrayToString(privateKey), CryptoCurrency.ByteArrayToString(publicKey), address, "first");
        }
    }
}
