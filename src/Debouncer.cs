namespace Debouncer;

public static class Debouncer
{
    public static IDebounce Debounce(Action invocation, int timeout)
    {
        return new Debounce(_ => invocation(), timeout);
    }

    public static IDebounce Debounce(Action<CancellationToken> invocation, int timeout)
    {
        return new Debounce(invocation, timeout);
    }

    public static IAsyncDebounce Debounce(Func<Task> invocation, int timeout)
    {
        return new AsyncDebounce(_ => invocation(), timeout);
    }

    public static IAsyncDebounce Debounce(Func<CancellationToken, Task> invocation, int timeout)
    {
        return new AsyncDebounce(invocation, timeout);
    }
}