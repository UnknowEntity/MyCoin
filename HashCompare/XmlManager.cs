using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HashCompare
{
    class XmlManager
    {
        private static readonly string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\UserInfo.xml";

        public static void NewKey(string privateKey, string publicKey, string address, string type)
        {
            float amount = 0;

            if (type=="first")
            {
                amount = 100;
            }

            XDocument doc = XDocument.Load(filePath);
            XElement element = doc.Element("UserInfo").Element("Keys");
            XElement key = new XElement("Key");
            key.Add(new XElement("DateCreated", DateTime.Now.ToLongDateString()));
            key.Add(new XElement("PrivateKey", privateKey));
            key.Add(new XElement("PublicKey", publicKey));
            key.Add(new XElement("Address", address));
            key.Add(new XElement("TransactionId", CryptoCurrency.ByteArrayToString(CryptoCurrency.StringToSha256(publicKey))));

            key.Add(new XElement("Index", element.Elements("Key").Count().ToString()));

            List<XAttribute> attribute = new List<XAttribute>()
            {
                new XAttribute("type", type),
                new XAttribute("amount", amount.ToString()),
                new XAttribute("isConfirm","false"),
                new XAttribute("unSpend","true")
            };

            key.Element("Address").Add(attribute);
            element.Add(key);
            doc.Save(filePath);
        }

        

        public static void NewXmlFile()
        {
            XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement root = new XElement("UserInfo");
            //root.Add(new XElement("Http", "https://blockchain-node-01.herokuapp.com/"));
            root.Add(new XElement("Http", "http://localhost:3000/"));
            root.Add(new XElement("Keys"));
            root.Add(new XElement("Transactions"));
            document.Add(root);
            document.Save(filePath);
        }

        public static bool IsExist()
        {
            if (File.Exists(filePath))
            {
                return true;
            }

            return false;
        }

        public static void SetHttp(string url)
        {
            XDocument doc = XDocument.Load(filePath);
            doc.Element("UserInfo").Element("Http").Value = url;
            doc.Save(filePath);
        }

        public static string GetHttp()
        {
            XDocument doc = XDocument.Load(filePath);
            return doc.Element("UserInfo").Element("Http").Value;
        }

        public static List<string> GetUnconfirm()
        {
            XDocument doc = XDocument.Load(filePath);
            List<XElement> elements = new List<XElement>(doc.Element("UserInfo").Element("Keys").Elements("Key"));

            List<string> result = new List<string>();

            foreach(XElement element in elements)
            {
                XElement temp = element.Element("Address");
               if (temp.Attribute("isConfirm").Value == "false")
                {
                    result.Add(temp.Value);
                }
            }

            return result;
        }

        public static float GetCurrentTotal()
        {
            XDocument doc = XDocument.Load(filePath);
            List<XElement> elements = new List<XElement>(doc.Element("UserInfo").Element("Keys").Elements("Key"));

            float total = 0;

            foreach (XElement element in elements)
            {
                XElement temp = element.Element("Address");
                if (temp.Attribute("unSpend").Value == "true")
                {
                    if (temp.Attribute("isConfirm").Value == "true")
                    {
                        total += float.Parse(temp.Attribute("amount").Value);
                    }
                }
            }

            return total;
        }

        public static List<Input> GetTotal(float amount, Span<float> change)
        {
            XDocument doc = XDocument.Load(filePath);
            List<XElement> elements = new List<XElement>(doc.Element("UserInfo").Element("Keys").Elements("Key"));

            List<Input> inputs = new List<Input>();

            int index = 0;
            float total = 0;

            while (index<elements.Count && total < amount)
            {
                XElement temp = elements[index].Element("Address");
                XElement privateKey = elements[index].Element("PrivateKey");
                if (temp.Attribute("unSpend").Value == "true")
                {
                    if (temp.Attribute("isConfirm").Value == "true")
                    {
                        total += float.Parse(temp.Attribute("amount").Value);
                        inputs.Add(new Input(temp.Value, CryptoCurrency.ByteArrayToIntArray(CryptoCurrency.Sign(temp.Value, CryptoCurrency.StringToByteArray(privateKey.Value)))));
                    }
                }
                index++;
            }

            change[0] = total - amount;

            return inputs;
        }

        

        public static void SetUnconfirm(Confirm[] data)
        {
            XDocument doc = XDocument.Load(filePath);
            List<XElement> elements = new List<XElement>(doc.Element("UserInfo").Element("Keys").Elements("Key"));
            

            foreach(Confirm confirm in data)
            {
                foreach (XElement element in elements)
                {
                    XElement temp = element.Element("Address");
                    if (temp.Value == confirm.Address)
                    {
                        temp.Attribute("isConfirm").Value = "true";
                        temp.Attribute("amount").Value = confirm.Amount.ToString();
                    }
                }
            }

            doc.Save(filePath);
        }

        public static void SetSpend(List<Input> inputs)
        {
            XDocument doc = XDocument.Load(filePath);
            List<XElement> elements = new List<XElement>(doc.Element("UserInfo").Element("Keys").Elements("Key"));


            foreach (Input input in inputs)
            {
                foreach (XElement element in elements)
                {
                    XElement temp = element.Element("Address");
                    if (temp.Value == input.Address)
                    {
                        temp.Attribute("unSpend").Value = "false";
                    }
                }
            }

            doc.Save(filePath);
        }

        public static void SaveTransaction(string id)
        {
            XDocument doc = XDocument.Load(filePath);
            doc.Element("UserInfo").Element("Transactions").Add(new XElement("Transaction", id));
            doc.Save(filePath);
        }

        public static List<string> GetAllTransaction()
        {
            List<string> idArray = new List<string>();
            XDocument doc = XDocument.Load(filePath);
            var transactions = doc.Element("UserInfo").Element("Transactions").Elements("Transaction");
            foreach(XElement transaction in transactions)
            {
                idArray.Add(transaction.Value);
            }

            return idArray;
        }
    }
}
