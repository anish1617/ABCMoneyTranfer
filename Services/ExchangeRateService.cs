﻿using Newtonsoft.Json;
using System.Text.Json;

namespace ABCMoneyTransfer.Services;

public class ExchangeRateService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ExchangeRateService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PayloadResponse> GetExchangeRatesAsync(string fromDate, string toDate, int page = 1, int perPage = 5)
    {
        var client = _httpClientFactory.CreateClient("ExchangeService");

        var response = await client.GetAsync($"api/forex/v1/rates?page={page}&per_page={perPage}&from=2024-06-12&to=2024-06-12");

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var exchangeRates = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
            return exchangeRates?.Data?.Payload.FirstOrDefault();
            
        }

        return null;
    }


    public async Task<decimal> GetExchangeRateOfCountry(DateTime date, string currency)
    {
        var client = _httpClientFactory.CreateClient("ExchangeService");

        var response = await client.GetAsync($"api/forex/v1/rates?page=1&per_page=5&from={date:yyyy-MM-dd}&to={date:yyyy-MM-dd}");

        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var rateData = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(content, options);

        var rates = rateData?.Data?.Payload.FirstOrDefault();
        var myrRate = rates?.Rates?.FirstOrDefault(r => r.Currency.Iso3 == currency);
        return Decimal.Parse(myrRate?.Buy);
    }
}


// Models to represent the API response
public class ApiResponse
{
    public ApiStatus Status { get; set; }
    public ApiErrors Errors { get; set; }
    public ApiParams Params { get; set; }
    public ApiData Data { get; set; }
}

public class ApiStatus
{
    public int Code { get; set; }
}

public class ApiErrors
{
    public object Validation { get; set; }
}

public class ApiParams
{
    public string From { get; set; }
    public string To { get; set; }
    public string PerPage { get; set; }
    public string Page { get; set; }
}

public class ApiData
{
    public List<PayloadResponse> Payload { get; set; }
    public Pagination Pagination { get; set; }
}

public class PayloadResponse
{
    public string Date { get; set; }
    public List<ExchangeRate> Rates { get; set; }
}

public class ExchangeRate
{
    public Currency Currency { get; set; }
    public string Buy { get; set; }
    public string Sell { get; set; }
}

public class Currency
{
    public string Iso3 { get; set; }
    public string Name { get; set; }
    public int Unit { get; set; }
}

public class Pagination
{
    public int Page { get; set; }
    public int Pages { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public PaginationLinks Links { get; set; }
}

public class PaginationLinks
{
    public string Prev { get; set; }
    public string Next { get; set; }
}