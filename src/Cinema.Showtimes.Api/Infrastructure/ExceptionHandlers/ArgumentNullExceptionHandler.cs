namespace Cinema.Showtimes.Api.Infrastructure.ExceptionHandlers;

public sealed class ArgumentNullExceptionHandler
{
    internal ArgumentNullExceptionHandler()
    {
    }

    public void If(bool condition, string argumentName, Func<string> exceptionMessage)
    {
        if (condition)
            throw new ArgumentNullException(argumentName, exceptionMessage());
    }

    public void IfNot(bool condition, string argumentName, Func<string> exceptionMessage)
    {
        if (!condition)
            throw new ArgumentNullException(exceptionMessage(), argumentName);
    }

    public TValue IfNull<TValue>(TValue value, string argumentName, Func<string> exceptionMessage = null)
    {
        If(value == null,
            argumentName,
            exceptionMessage != null
                ? exceptionMessage
                : () => $"{argumentName} can not be null.");

        return value;
    }
}