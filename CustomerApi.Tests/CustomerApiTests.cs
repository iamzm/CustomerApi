using CustomerAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Tests
{
    public class CustomerApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CustomerApiTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Empty_Customers_Returns_Empty_List()
        {
            var response = await _client.GetAsync("/api/customers");
            response.EnsureSuccessStatusCode();

            var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
            Assert.NotNull(customers);
            Assert.Empty(customers);
        }

        [Fact]
        public async Task Create_And_Get_Customer_Works()
        {
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/customers", customer);
            postResponse.EnsureSuccessStatusCode();

            var created = await postResponse.Content.ReadFromJsonAsync<Customer>();
            Assert.NotNull(created);
            Assert.Equal(customer.Email, created!.Email);

            var getResponse = await _client.GetAsync($"/api/customers/{created.Id}");
            getResponse.EnsureSuccessStatusCode();

            var fetched = await getResponse.Content.ReadFromJsonAsync<Customer>();
            Assert.Equal(created.Id, fetched!.Id);
        }

        [Fact]
        public async Task Update_Customer_Works()
        {
            var customer = new Customer
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                PhoneNumber = "999-888-7777"
            };

            var created = await (await _client.PostAsJsonAsync("/api/customers", customer))
                .Content.ReadFromJsonAsync<Customer>();

            created!.FirstName = "Janet";
            var putResponse = await _client.PutAsJsonAsync($"/api/customers/{created.Id}", created);
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);

            var fetched = await (await _client.GetAsync($"/api/customers/{created.Id}"))
                .Content.ReadFromJsonAsync<Customer>();

            Assert.Equal("Janet", fetched!.FirstName);
        }

        [Fact]
        public async Task Delete_Customer_Works()
        {
            var customer = new Customer
            {
                FirstName = "Mark",
                LastName = "Lee",
                Email = "mark.lee@example.com",
                PhoneNumber = "333-222-1111"
            };

            var created = await (await _client.PostAsJsonAsync("/api/customers", customer))
                .Content.ReadFromJsonAsync<Customer>();

            var deleteResponse = await _client.DeleteAsync($"/api/customers/{created!.Id}");
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

            var getResponse = await _client.GetAsync($"/api/customers/{created.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
        }
    }
}
