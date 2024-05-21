using Timer = System.Timers.Timer;

namespace Debouncer;

internal abstract class DebounceBase(int timeout)
{
    private CancellationTokenSource _cancellationTokenSource = new();
    private Timer? _timer;

    public int Timeout { get; } = timeout;

    public virtual void Cancel()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();

        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected void Invoke(Action<CancellationToken> action)
    {
        _timer?.Dispose();

        _timer = new Timer(Timeout);
        _timer.AutoReset = false;
        _timer.Elapsed += (_, _) => { action(_cancellationTokenSource.Token); };
        _timer.Start();
    }
}