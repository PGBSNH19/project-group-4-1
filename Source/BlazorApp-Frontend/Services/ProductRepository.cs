﻿using BlazorApp_Frontend.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp_Frontend.Services
{
    public class ProductRepository
    {
        public HttpClient http { get; }
        public ProductRepository(HttpClient client)
        {
            client.BaseAddress = new Uri("https://nearbyproduceapiTest.azurewebsites.net");
            http = client;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await http.GetJsonAsync<List<Product>>(http.BaseAddress + "/api/v1.0/Product/GetProducts");
            return products;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await http.GetJsonAsync<Product>(http.BaseAddress + $"/api/v1.0/Product/GetProduct/{id}");

            return product;
        }
        public async Task<Product> PostProduct(Product productToCreate)
        {
            var data = new StringContent(JsonConvert.SerializeObject(productToCreate), Encoding.UTF8, "application/json");

            var product = await http.PostJsonAsync<Product>(http.BaseAddress + "api/v1.0/Product", data);
            return product;
        }
    }
}
