using BlazorApp_Frontend.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace BlazorApp_Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddOptions();
            services.AddAuthorizationCore();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<HttpClient>();
            services.AddSingleton<UserRepository>();
            services.AddSingleton<MarketplaceRepository>();
            services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri("https://nearbyproduceapitest.azurewebsites.net");
            });
            services.AddScoped<LocalAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<LocalAuthenticationStateProvider>());
            services.AddAuthorization(config =>
            {
                config.AddPolicy("IsBuyer", policy => policy.RequireClaim("Type", "Buyer"));
                config.AddPolicy("IsSeller", policy => policy.RequireClaim("Type", "Seller"));
            });
            services.AddSingleton<ProductRepository>();
            services.AddSingleton<SellerPageRepository>();
            services.AddSingleton<UserService>();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
