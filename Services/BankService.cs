using Newtonsoft.Json;

namespace ABCMoneyTransfer.Services
{
    public class BankService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BankService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<Bank>> GetBanksOfNepal()
        {
            var client = _httpClientFactory.CreateClient("WiseApi");

            var response = await client.GetAsync($"/v1/banks?country=NP");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BankWrapper>(content);

                return result.Values.ToList();
            }

            return null;
        }
    }
}


public class BankWrapper
{
    public List<Bank> Values { get; set; }
}

public class Bank
{
    public string Code { get; set; }
    public string Title { get; set; }
}