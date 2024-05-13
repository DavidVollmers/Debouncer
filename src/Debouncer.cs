namespace Debouncer;

public static class Debouncer
{
    public static IDebounce Debounce(Action invocation, int timeout)
    {
    }

    public static IDebounce Debounce(Func<CancellationToken> invocation, int timeout)
    {
    }

    public static IAsyncDebounce Debounce(Func<Task> invocation, int timeout)
    {
    }

    public static IAsyncDebounce Debounce(Func<CancellationToken, Task> invocation, int timeout)
    {
    }
}