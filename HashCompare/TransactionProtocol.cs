using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HashCompare
{

    class TransactionProtocol
    {
        public string URL { set; get; }
        public string Response { set; get; }

        private static readonly HttpClient client = new HttpClient();

        public TransactionProtocol()
        {
            URL = XmlManager.GetHttp();
            Response = "";
        }
        
        public async Task MakeTransaction(Transaction transaction)
        {
           

            var myData = JsonConvert.SerializeObject(transaction);
            var buff = Encoding.UTF8.GetBytes(myData);
            var content = new ByteArrayContent(buff);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Response = await PostTask(content, "transaction");
            
        }

        public async Task GetCurrentTotal()
        {
            List<string> data = XmlManager.GetUnconfirm();



            var myData = JsonConvert.SerializeObject(data);
            var buff = Encoding.UTF8.GetBytes(myData);
            var content = new ByteArrayContent(buff);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Response= await PostTask(content,"getconfirm");

            Confirm[] get = JsonConvert.DeserializeObject<Confirm[]>(Response);

            XmlManager.SetUnconfirm(get);
            
        }


        private async Task<string> PostTask(HttpContent content, string route)
        {
            var response = await client.PostAsync($"{URL}{route}", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}
