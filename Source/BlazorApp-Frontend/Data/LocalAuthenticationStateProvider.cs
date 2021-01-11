﻿using BlazorApp_Frontend.Data;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorApp_Frontend
{
    public class LocalAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storageService;
        public LocalAuthenticationStateProvider(ILocalStorageService storageService)
        {
            _storageService = storageService;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if(await _storageService.ContainKeyAsync("User"))
            {
                // Create user
                var userInfo = await _storageService.GetItemAsync<LocalUserInfo>("User");

                var claims = new[]
                {
                    new Claim("Email", userInfo.Email),
                    new Claim("UserName", userInfo.UserName),
                    new Claim("AcessToken", userInfo.AccessToken),
                    new Claim("Type", userInfo.Type),
                    new Claim("Id", userInfo.Id),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id),
                };

                var identity = new ClaimsIdentity(claims, "BearerToken");
                var user = new ClaimsPrincipal(identity);
                var state = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(state));
                return state;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
        public async Task LogoutAsync()
        {
            await _storageService.RemoveItemAsync("User");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}
