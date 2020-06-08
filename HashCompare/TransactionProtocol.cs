using EasyAsyncCancel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HashCompare
{

    class TransactionProtocol
    {
        public string URL { set; get; }
        public string Response { set; get; }

        private static readonly HttpClient client = new HttpClient();
        private CancellationTokenSource cts;

        public TransactionProtocol()
        {
            URL = XmlManager.GetHttp();
            Response = "";
        }
        
        public async Task<TransactionResponse> MakeTransaction(Transaction transaction)
        {
           

            var myData = JsonConvert.SerializeObject(transaction);
            var buff = Encoding.UTF8.GetBytes(myData);
            var content = new ByteArrayContent(buff);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Response = await PostTask(content, "transaction");
            TransactionResponse transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(Response);

            if (transactionResponse.Status=="valid")
            {
                XmlManager.SaveTransaction(transactionResponse.Id);
                if(transaction.Type!="first")
                {
                    XmlManager.SetSpend(transaction.Inputs);
                }
                return transactionResponse;
            }
            
            return transactionResponse;
            
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

        public async Task<TransactionJSON[]> GetAllTransaction()
        {
            List<string> data = XmlManager.GetAllTransaction();



            var myData = JsonConvert.SerializeObject(data);
            var buff = Encoding.UTF8.GetBytes(myData);
            var content = new ByteArrayContent(buff);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            Response = await PostTask(content, "gettransaction");

            TransactionJSON[] get = JsonConvert.DeserializeObject<TransactionJSON[]>(Response);

            return get;
        }

        public async Task<string[]> GetAllNode()
        {
            cts = new CancellationTokenSource();

            

            try
            {
                cts.CancelAfter(5000);

                Response = await GetTask("getnodelist", cts.Token);

                string[] get = JsonConvert.DeserializeObject<string[]>(Response);
                cts = null;
                return get;

            }
            catch (OperationCanceledException)
            {
                string[] falls = new string[1] { "" };
                cts = null;
                return falls;
            }
            catch (Exception)
            {
                string[] falls = new string[1] { "" };
                cts = null;
                return falls;
            }
        }

        public async Task<bool> CheckServer(string url)
        {
            cts = new CancellationTokenSource();

            try
            {
                cts.CancelAfter(5000);

                Response = await CheckServerAsync(url,"check",cts.Token);

                bool get = JsonConvert.DeserializeObject<bool>(Response);
                cts = null;
                return get;

            }
            catch (OperationCanceledException)
            {
                cts = null;
                return false;
            }
            catch (Exception)
            {
                cts = null;
                return false;
            }

            
        }

        private async Task<string> CheckServerAsync(string url,string route, CancellationToken ct)
        {
            var response = await client.GetAsync($"{url}{route}", ct);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private async Task<string> GetTask(string route, CancellationToken ct)
        {
            var response = await client.GetAsync($"{URL}{route}",ct);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

        private async Task<string> PostTask(HttpContent content, string route)
        {
            var response = await client.PostAsync($"{URL}{route}", content);

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }

    }
}
