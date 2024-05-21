namespace Debouncer;

internal class Debounce(Action<CancellationToken> invocation, int timeout) : DebounceBase(timeout), IDebounce
{
    private readonly List<Action<CancellationToken>> _invocations = [];

    public override void Cancel()
    {
        _invocations.Clear();

        base.Cancel();
    }

    public void Flush()
    {
        foreach (var i in _invocations)
        {
            i(default);
        }

        _invocations.Clear();
    }

    public void Invoke()
    {
        _invocations.Add(invocation);

        Invoke(cancellationToken =>
        {
            _invocations.Remove(invocation);
            
            if (!cancellationToken.IsCancellationRequested)
            {
                invocation(cancellationToken);
            }
        });
    }
}