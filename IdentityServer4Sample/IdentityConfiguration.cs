using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer4Sample
{
    public class IdentityConfiguration
    {
        public static List<TestUser> TestUsers =>
    new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1144",
            Username = "mukesh",
            Password = "mukesh",
            Claims =
            {
                new Claim(JwtClaimTypes.Name, "Mukesh Murugan"),
                new Claim(JwtClaimTypes.GivenName, "Mukesh"),
                new Claim(JwtClaimTypes.FamilyName, "Murugan"),
                new Claim(JwtClaimTypes.WebSite, "http://codewithmukesh.com"),
            }
        }
      };
    public static IEnumerable<IdentityResource> IdentityResources =>
    new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
    };
        public static IEnumerable<ApiScope> ApiScopes =>
    new ApiScope[]
    {
        new ApiScope("myApi.read"),
        new ApiScope("myApi.write"),
    };
    }
}
