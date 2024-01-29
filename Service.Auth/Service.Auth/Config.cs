using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;

namespace Service.Auth;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("catalog", "Catalog API")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "clientId1",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "catalog" },
                AllowOfflineAccess = true, // Enables support for refresh tokens
                RefreshTokenUsage = TokenUsage.ReUse,
            }
        };

    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "Buyer",
                Password = "password1",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Role, "Buyer")
                },
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "Manager",
                Password = "password2",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Role, "Manager")
                }
            }
        };
}