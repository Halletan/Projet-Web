using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using NuGet.Protocol;

namespace SpaceAdventures.MVC.Configurations
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection AddAuthenticationServiceCollection(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect("Auth0", options =>
                {
                    // Set the authority to your Auth0 domain
                    options.Authority = $"https://{configuration["Auth0:Domain"]}";

                    // Configuring Auth0 Client Id and Secret
                    options.ClientId = configuration["Auth0:ClientId"];
                    options.ClientSecret = configuration["Auth0:ClientSecret"];

                    //Setting response type to our code
                    options.ResponseType = OpenIdConnectResponseType.Code;

                    // Configuring the scope
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("read:messages");
                    options.CallbackPath = new PathString("/callback");
                    options.ClaimsIssuer = "AuthO"; // Allow the AuthO as authenticator
                    options.SaveTokens = true;   // In order to be able to retrieve the Access Token to authenticate the user in calls to your API
                    options.Events = new OpenIdConnectEvents
                    {

                        // ASP.NET Core calls SignOutAsync for the "Auth0" authentication scheme.
                        // We need to provide the OIDC middleware with the URL for logging the user out of Auth0. To set the URL,
                        // handle the OnRedirectToIdentityProviderForSignOut event when we register the OIDC authentication handler.


                        //When the application calls SignOutAsync for the OIDC middleware,
                        //it also calls the /v2/logout endpoint of the Auth0 Authentication API, which will ensure the user is logged out of Auth0.


                        OnRedirectToIdentityProviderForSignOut = (context) =>
                        {
                            var logoutUri =
                                $"https://{configuration["Auth0:Domain"]}/v2/logout?client_id={configuration["Auth0:ClientId"]}";
                            var postLogoutUri = context.Properties.RedirectUri;
                            if (!string.IsNullOrEmpty(postLogoutUri))
                            {
                                if (postLogoutUri.StartsWith("/"))
                                {
                                    // Transform to absolute
                                    var request = context.Request;
                                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase +
                                                    postLogoutUri;
                                }

                                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
                            }

                            context.Response.Redirect(logoutUri);
                            context.HandleResponse();

                            return Task.CompletedTask;
                        },

                        OnRedirectToIdentityProvider = (context) =>
                        {

                            // Set the audience query parameter to the API identifier to ensure the returned access tokens can be used 
                            // to call protected endpoints on the API.

                            context.ProtocolMessage.SetParameter("audience", configuration["Auth0:Audience"]);
                            return Task.FromResult(0);
                        },

                        OnMessageReceived = (context) =>
                        {
                            if (context.ProtocolMessage.Error == "access_denied")
                            {
                                context.HandleResponse();
                                context.Response.Redirect("/Account/AccessDenied");
                            }
                            return Task.FromResult(0);
                        }
                    };
                });

            return services;
        }
    }
}
