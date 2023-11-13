using System.Collections;

namespace Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;

public sealed class ArgumentExceptionHandler
{
    internal ArgumentExceptionHandler()
    {
    }

    public void If(bool condition, string argumentName, Func<string> exceptionMessage)
    {
        if (condition)
            throw new ArgumentException(exceptionMessage(), argumentName);
    }

    public void IfNot(bool condition, string argumentName, Func<string> exceptionMessage)
    {
        if (!condition)
            throw new ArgumentException(exceptionMessage(), argumentName);
    }

    public string IfNullOrWhiteSpace(string value, string argumentName, Func<string> exceptionMessage = null)
    {
        If(string.IsNullOrWhiteSpace(value),
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} can not be null or whitespace.");

        return value;
    }

    public TValue IfDefault<TValue>(TValue value, string argumentName, Func<string> exceptionMessage = null)
    {
        If(value.Equals(default(TValue)),
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} can not be default value.");

        return value;
    }

    public DateTime IfLocalOrUnspecified(DateTime value, string argumentName, Func<string> exceptionMessage = null)
    {
        If(value.Kind != DateTimeKind.Utc,
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} must be UTC datetime kind.");

        return value;
    }

    public TArg IfEmpty<TArg>(
        TArg value,
        string argumentName,
        Func<string> exceptionMessage = null)
        where TArg : ICollection
    {
        If(value.Count == 0,
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} can not be empty.");

        return value;
    }

    public TArg IfNotEmpty<TArg>(
        TArg value,
        string argumentName,
        Func<string> exceptionMessage = null)
        where TArg : ICollection
    {
        IfNot(value.Count == 0,
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} must be empty.");

        return value;
    }

    public string IfTooLong(
        string value,
        int maxLength,
        string argumentName,
        Func<string> exceptionMessage = null)
    {
        if (value == null)
            return null;

        If(value.Length > maxLength,
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} length cannot exceed {maxLength}.");

        return value;
    }

    public string IfNullOrWhiteSpaceOrTooLong(
        string value,
        int maxLength,
        string argumentName,
        Func<string> exceptionMessage = null)
    {
        IfNullOrWhiteSpace(value, argumentName, exceptionMessage);
        return IfTooLong(value, maxLength, argumentName, exceptionMessage);
    }
}