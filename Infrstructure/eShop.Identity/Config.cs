using Duende.IdentityServer.Models;

namespace eShop.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("catalogapi.read"),
            new ApiScope("catalogapi.write"),
            new ApiScope("basketapi"),
            new ApiScope("eshopgateway"),
        };
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("Catalog", "Catalog.API")
            {
                Scopes =
                {
                    "catalogapi.read",
                    "catalogapi.write"
                }
            },
             new ApiResource("Basket", "Basket.API")
            {
                Scopes =
                {
                    "basketapi"
                }
            },
            new ApiResource("EshopGateway", "Eshop Gateway API")
            {
                Scopes =
                {
                    "eshopgateway",
                    "basketapi"
                }
            }
        };
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    
                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "scope2" }
            },
            new Client
            {
                ClientName="Catalog API Client",
                ClientId ="calalogapiclient",
                ClientSecrets =  { new Secret("49C1A7E1-0C79-4A09-A3D6-A37998FB86A0".Sha256()) },
                AllowedGrantTypes =GrantTypes.ClientCredentials,
                AllowedScopes = { "catalogapi.write", "catalogapi.read" }

            },
             new Client
            {
                ClientName="Basket API Client",
                ClientId ="basketapiclient",
                ClientSecrets =  { new Secret("49C1A7E1-0C79-5B09-A3D6-A37998FB86A0".Sha256()) },
                AllowedGrantTypes =GrantTypes.ClientCredentials,
                AllowedScopes = {   "eshopgateway", "basketapi" }

            },
             new Client
             {
                    ClientName="Eshop Gateway Client",
                    ClientId ="eshopgatewayclient",
                    ClientSecrets =  { new Secret("49C1A7E1-0C79-5B09-A3D7-W37998FB86A0".Sha256()) },
                    AllowedGrantTypes =GrantTypes.ClientCredentials,
                    AllowedScopes = {  "basketapi" }
             }
        };
}
