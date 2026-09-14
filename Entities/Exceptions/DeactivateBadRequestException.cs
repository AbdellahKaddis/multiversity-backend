using System;


namespace Entities.Exceptions;

public sealed class DeactivateBadRequestException : BadRequestException
{
    public DeactivateBadRequestException()
    : base("User already deactivated.")
    {
    }
}

