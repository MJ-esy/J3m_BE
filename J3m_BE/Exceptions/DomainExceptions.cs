namespace J3m_BE.Exceptions;

// Base exception class for domain-related errors

// Exception for domain-related errors
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

// Exception for not found domain entities
public class NotFoundDomainException : DomainException
{
    public NotFoundDomainException(string message) : base(message) { }
}

// Exception for conflict-related domain errors
public class ConflictDomainException : DomainException
{
    public ConflictDomainException(string message) : base(message) { }
}

//Exception for validation-related domain errors
public class ValidationDomainException : DomainException
{
    public ValidationDomainException(string message) : base(message) { }
}