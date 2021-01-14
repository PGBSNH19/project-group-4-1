using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace API.Services
{
    public class AzureKeyvaultService
    {
        public static string GetKeyVaultSecret(string secretName)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVault = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            var secret = keyVault.GetSecretAsync(secretName).Result;
            return secret.Value;
        }
    }
}
