using Newtonsoft.Json;
using PaymentProcessorClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentProcessorClient.Server
{
    public static class ServerApi
    {

        private static readonly HttpClient client = new();
        private static readonly string SERVER_URL = "http://localhost:33000";

        public static async Task<List<Transaction>> GetUnpaidTransactions()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{SERVER_URL}/Transactions/GetAllUnpaidTransactions");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<List<Transaction>>(content);
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return new List<Transaction>();
        }

        public static async Task<Account?> GetAccountById(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{SERVER_URL}/Accounts/GetAccountById/{id}");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Account>(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public static async Task MakePayments(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync($"{SERVER_URL}/Payments/MakePayment/{id}", null);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
