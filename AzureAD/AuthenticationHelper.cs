using System;
using System.Web;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using System.IO;

namespace AzureAD
{
    internal class AuthenticationHelper
    {
        public static string tocken;

        public static bool DoReset = false;

        protected static string filePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "oo365.tk");
            }
        }

        public static bool load()
        {
            if (!System.IO.File.Exists(filePath)) return false;
            tocken = System.IO.File.ReadAllText(filePath);
            return true;
        }

        public static void save()
        {
            System.IO.File.WriteAllText(filePath, tocken??"");
        }

        public static async Task<string> GetTocken()
        {
            if (DoReset)
            {
                tocken = null;
            }
            if (tocken == null)
            {
                if ((DoReset) || !load())
                {
                    AuthenticationResult userAuthnResult;
                    Uri redirectUri = new Uri("https://localhost");
                    AuthenticationContext authenticationContext = new AuthenticationContext(eduTenant.AuthString, false);
                    userAuthnResult = await authenticationContext.AcquireTokenAsync(eduTenant.ResourceUrl,
                        eduTenant.ClientIdForUserAuthn, redirectUri, new PlatformParameters(PromptBehavior.Always));
                    tocken = userAuthnResult.AccessToken;
                    save();
                    DoReset = false;
                }
            }
            return tocken;
        }

        /// <summary>
        /// Get Graph Service Client for User.
        /// </summary>
        /// <returns>GraphServiceClient for User.</returns>
        public static async Task<GraphServiceClient> GetGraphServiceClientAsUser()
        {
            string accessToken = await GetTocken();

            GraphServiceClient GraphServiceClient = 
            new GraphServiceClient(eduTenant.ResourceUrl + "/" + eduTenant.TenantId,
            new DelegateAuthenticationProvider(
            (requestMessage) =>
            {
                requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);
                UriBuilder ub = new UriBuilder(requestMessage.RequestUri);
                System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(ub.Query);
                query["api-version"] = "1.5";
                ub.Query = query.ToString();
                requestMessage.RequestUri = ub.Uri;
                return Task.FromResult(0);
            }));

            return GraphServiceClient;
        }

        public async static Task<ActiveDirectoryClient> GetActiveDirectoryClientAsUser()
        {
            string accessToken = await GetTocken();

            Uri servicePointUri = new Uri(eduTenant.ResourceUrl);
            Uri serviceRoot = new Uri(servicePointUri, eduTenant.TenantId);
            ActiveDirectoryClient activeDirectoryClient = new ActiveDirectoryClient(serviceRoot, GetTocken);
            return activeDirectoryClient;
        }


    }
}
