using MedsWebApp;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTests
{
    public class Tests
    {
        private HttpClient client;
        private string access_token;
        private string refresh_token;
        private int medId;
        private int medInPharId;
        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Startup>();
            client = factory.CreateClient();
        }

        [Test, Order(1)]
        public async Task Register()
        {
            var json = JsonConvert.SerializeObject(new { login = "drive201030@gmail.com", password = "1234", name = "ƒмитро" });
            var response = await client.PostAsync("api/register", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) Assert.AreEqual(errorString, "user is already exists");
                Assert.Fail(response.StatusCode.ToString() + " " + errorString);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var type = new
            {
                user_name = string.Empty,
                access_token = string.Empty,
                access_token_expire = DateTime.MinValue,
                refresh_token = string.Empty,
                refresh_token_expire = DateTime.MinValue,
                is_email_sended = false
            };
            var jsonObj = JsonConvert.DeserializeAnonymousType(jsonResponse, type);
            access_token = jsonObj.access_token;
            refresh_token = jsonObj.refresh_token;
            Assert.IsNotEmpty(access_token, jsonResponse);
        }

        [Test, Order(2)]
        public async Task Login()
        {
            var json = JsonConvert.SerializeObject(new { login = "drive201030@gmail.com", password = "1234" });
            var response = await client.PostAsync("api/login", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Assert.Fail(response.StatusCode.ToString() + errorString);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var type = new
            {
                user_name = string.Empty,
                access_token = string.Empty,
                access_token_expire = DateTime.MinValue,
                refresh_token = string.Empty,
                refresh_token_expire = DateTime.MinValue
            };
            var jsonObj = JsonConvert.DeserializeAnonymousType(jsonResponse, type);
            access_token = jsonObj.access_token;
            refresh_token = jsonObj.refresh_token;
            Assert.IsNotEmpty(jsonObj.access_token, jsonResponse);
        }
        [Test]
        public async Task RefreshToken()
        {
            var response = await client.GetAsync($"api/refreshtoken/{refresh_token}");
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Assert.Fail(response.StatusCode.ToString() + errorString);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var type = new
            {
                user_name = string.Empty,
                access_token = string.Empty,
                access_token_expire = DateTime.MinValue,
                refresh_token = string.Empty,
                refresh_token_expire = DateTime.MinValue
            };
            var jsonObj = JsonConvert.DeserializeAnonymousType(jsonResponse, type);
            access_token = jsonObj.access_token;
            refresh_token = jsonObj.refresh_token;
            Assert.IsNotEmpty(jsonObj.access_token, jsonResponse);
        }
        [Test]
        public async Task ResendRegistrationEmail()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            var response = await client.GetAsync($"api/resendregistrationemail");
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Assert.Fail(response.StatusCode.ToString() + errorString);
            }
            else Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test, Order(3)]
        public async Task Search()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            int page = 1;
            var response = await client.GetAsync($"api/search/јм≥зончик/{page}");
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Assert.Fail(response.StatusCode.ToString() + errorString);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var type = new
            {
                meds = new[]
                {
                    new
                    {
                        Id = -1,
                        Name = string.Empty,
                        CategoryId = -1,
                        CategoryName = string.Empty,
                        ManufacturerId = -1,
                        ManufacturerName = string.Empty,
                        Description = string.Empty,
                        ImageURL = string.Empty
                    },
                    new
                    {
                        Id = -1,
                        Name = string.Empty,
                        CategoryId = -1,
                        CategoryName = string.Empty,
                        ManufacturerId = -1,
                        ManufacturerName = string.Empty,
                        Description = string.Empty,
                        ImageURL = string.Empty
                    }
                },
                page = -1,
                pageSize = -1,
                totalPages = -1
            };
            var jsonObj = JsonConvert.DeserializeAnonymousType(jsonResponse, type);
            medId = jsonObj.meds[0].Id;
            Assert.IsTrue(jsonObj.meds.Length == 1 || page == jsonObj.page);
        }
        

        [Test, Order(4)]
        public async Task GetMedicineInPharmacies()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            var response = await client.GetAsync($"api/GetMedicineInPharmacies/{medId}");
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Assert.Fail(response.StatusCode.ToString() + errorString);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var type = new
            {
                medInPharmacies = new[]
                {
                    new
                    {
                        Id = -1,
                        AvailableCount = -1,
                        DiscountId = new int?(1),
                        MedicineId = -1,
                        MedicineName = string.Empty,
                        PharmacyAddress = string.Empty,
                        PharmacyId = -1,
                        PharmacyName = string.Empty,
                        Price = 0.0
                    }
                }
            };
            var jsonObj = JsonConvert.DeserializeAnonymousType(jsonResponse, type);
            medInPharId = jsonObj.medInPharmacies[0].Id;
            Assert.IsTrue(jsonObj.medInPharmacies.Length == 4);
        }

        [Test, Order(5)]
        public async Task CreateOrder()
        {
            var order = new
            {
                items = new[]
                {
                    new {
                        medicineinpharmacyid = medInPharId,
                        count = 1
                    }
                }
            };
            var json = JsonConvert.SerializeObject(order);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
            var response = await client.PostAsync("api/createorder", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
            {
                var errorString = await response.Content.ReadAsStringAsync();
                Assert.Fail(response.StatusCode.ToString() + errorString);
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var type = new
            {
                orders = new[]
                {
                    new
                    {
                        Id = -1,
                        UserId = -1,
                        Create = DateTime.MinValue,
                        OrderItems = new[]
                        {
                            new
                            {
                                Id = -1,
                                Count = -1,
                                MedicineInPharmacyId = -1,
                                MedicineName = string.Empty,
                                PharmacyId = -1,
                                PharmacyName = string.Empty,
                                PharmacyAddress = string.Empty,
                                Price = 0.0,
                                PharmacyTel = string.Empty
                            }
                        },
                    Status = -1
                    }
                }
            };
            var jsonObj = JsonConvert.DeserializeAnonymousType(jsonResponse, type);
            Assert.IsTrue(jsonObj.orders[0].OrderItems[0].MedicineInPharmacyId == medInPharId);
        }
    }
}