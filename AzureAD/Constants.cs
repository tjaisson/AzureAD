using System;

namespace AzureAD
{

    internal class eduTenant
    {
        public const string ResourceUrl = "https://graph.windows.net";
        public static string AuthString { get { return "https://login.microsoftonline.com/" + TenantName; } }

        public const string TenantName = "edu.ac-paris.fr";
        public const string TenantId = "cfb2cddb-47aa-407b-858e-668d24828a69";
        public const string ClientId = "118473c2-7619-46e3-a8e4-6da8d5f56e12";
        public const string ClientSecret = "hOrJ0r0TZ4GQ3obp+vk3FZ7JBVP+TX353kNo6QwNq7Q=";
        public const string ClientIdForUserAuthn = "d5ac20ff-738d-4a5d-a8fe-0e8b3887be8a";
    }

    internal class elisaTenant
    {
        protected const string ResourceUrl = "https://graph.windows.net";
        public static string AuthString { get { return "https://login.microsoftonline.com/" + TenantName; } }

        public const string TenantName = "elemonnier.onmicrosoft.com";
        public const string TenantId = "c35550df-0534-4734-bf9a-c29f797d3cc9";
        public const string ClientId = "118473c2-7619-46e3-a8e4-6da8d5f56e12";
        public const string ClientSecret = "hOrJ0r0TZ4GQ3obp+vk3FZ7JBVP+TX353kNo6QwNq7Q=";
        public const string ClientIdForUserAuthn = "300100d3-82f4-46d8-a690-84b33038732a";
    }
}