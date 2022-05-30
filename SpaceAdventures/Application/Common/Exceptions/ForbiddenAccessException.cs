namespace SpaceAdventures.Application.Common.Exceptions;

public class ForbiddenAccessException : UnauthorizedAccessException
{
    public ForbiddenAccessException() : base("Forbidden Access : Either you don't have the right scope or you haven't provided a token at all") { }
}