namespace Debouncer;

internal class AsyncDebounce(Func<CancellationToken, Task> invocation, int timeout)
    : DebounceBase(timeout), IAsyncDebounce
{
    private readonly List<Func<CancellationToken, Task>> _invocations = [];

    public override void Cancel()
    {
        _invocations.Clear();

        base.Cancel();
    }

    public Task FlushAsync()
    {
        return Task.WhenAll(_invocations.Select(i => i(default)));
    }

    public Task InvokeAsync()
    {
        var taskCompletionSource = new TaskCompletionSource();

        _invocations.Add(invocation);

        Invoke(cancellationToken =>
        {
            _invocations.Remove(invocation);

            if (cancellationToken.IsCancellationRequested)
            {
                taskCompletionSource.SetCanceled(cancellationToken);
                return;
            }

            invocation(cancellationToken).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    taskCompletionSource.SetException(t.Exception!);
                }
                else if (t.IsCanceled)
                {
                    taskCompletionSource.SetCanceled(cancellationToken);
                }
                else
                {
                    taskCompletionSource.SetResult();
                }
            }, cancellationToken);
        });

        return taskCompletionSource.Task;
    }
}