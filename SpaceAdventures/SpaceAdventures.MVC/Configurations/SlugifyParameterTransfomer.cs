using System.Text.RegularExpressions;

namespace SpaceAdventures.MVC.Configurations
{
    public class SlugifyParameterTransfomer : IRouteConstraint
    {
        public string? TransformOutbound(object? value)
        {
            // Slugify value
            return value == null ? null : Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower();
        }

        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            throw new NotImplementedException();
        }
    }
}
