
namespace SpaceAdventures.Application.Common.Exceptions
{
    public class ForbiddenAccessException : UnauthorizedAccessException
    {
        public ForbiddenAccessException() : base("Unauthorized access, please provide a token") { }
    }
}
