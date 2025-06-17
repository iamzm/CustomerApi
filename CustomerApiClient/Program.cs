using CustomerApiClient.Models;
using System.Net.Http.Json;
using System.Text.Json;

HttpClient client = new()
{
    BaseAddress = new Uri("https://localhost:7077") // Change if your API runs elsewhere
};


await ListCustomersAsync();
await CreateCustomerAsync();
await UpdateCustomerAsync();
await DeleteCustomerAsync();

async Task ListCustomersAsync()
{
    Console.WriteLine("GET: All Customers");

    var customers = await client.GetFromJsonAsync<List<Customer>>("/api/customers");

    if (customers is { Count: > 0 })
    {
        foreach (var c in customers)
            Console.WriteLine($"{c.Id} | {c.FirstName} {c.LastName} | {c.Email}");
    }
    else
    {
        Console.WriteLine("No customers found.");
    }
}

async Task CreateCustomerAsync()
{
    Console.WriteLine("\nPOST: Creating a new customer");

    var newCustomer = new Customer
    {
        FirstName = "Alice",
        MiddleName = "Q.",
        LastName = "Doe",
        Email = "alice@example.com",
        PhoneNumber = "111-222-3333"
    };

    var response = await client.PostAsJsonAsync("/api/customers", newCustomer);

    if (response.IsSuccessStatusCode)
    {
        var created = await response.Content.ReadFromJsonAsync<Customer>();
        Console.WriteLine($"Created: {created?.Id} - {created?.Email}");
    }
    else
    {
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Create failed: {error}");
    }
}

async Task UpdateCustomerAsync()
{
    Console.WriteLine("\nPUT: Updating the first customer");

    var customers = await client.GetFromJsonAsync<List<Customer>>("/api/customers");
    var customer = customers?.FirstOrDefault();

    if (customer is null)
    {
        Console.WriteLine("No customers to update.");
        return;
    }

    customer.FirstName = "UpdatedName";

    var response = await client.PutAsJsonAsync($"/api/customers/{customer.Id}", customer);
    Console.WriteLine(response.IsSuccessStatusCode
        ? "Update successful"
        : $"Update failed: {await response.Content.ReadAsStringAsync()}");
}

async Task DeleteCustomerAsync()
{
    Console.WriteLine("\nDELETE: Removing the first customer");

    var customers = await client.GetFromJsonAsync<List<Customer>>("/api/customers");
    var customer = customers?.FirstOrDefault();

    if (customer is null)
    {
        Console.WriteLine("No customers to delete.");
        return;
    }

    var response = await client.DeleteAsync($"/api/customers/{customer.Id}");
    Console.WriteLine(response.IsSuccessStatusCode
        ? $"Deleted customer {customer.Id}"
        : $"Delete failed: {await response.Content.ReadAsStringAsync()}");
}
