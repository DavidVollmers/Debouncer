using Timer = System.Timers.Timer;

namespace Debouncer;

internal abstract class DebounceBase(int timeout)
{
    private CancellationTokenSource _cancellationTokenSource = new();
    private Action? _disposeCallback;
    private Timer? _timer;

    public int Timeout { get; } = timeout;

    public virtual void Cancel()
    {
        _cancellationTokenSource.Cancel();

        _cancellationTokenSource = new CancellationTokenSource();
    }

    protected void Invoke(Action<CancellationToken> action, Action disposeCallback)
    {
        Reset();

        // Capture the current CancellationToken until the next timeout
        var cancellationToken = _cancellationTokenSource.Token;
        
        _disposeCallback = disposeCallback;

        _timer = new Timer(Timeout);
        _timer.AutoReset = false;
        _timer.Elapsed += (_, _) => { action(cancellationToken); };
        _timer.Start();
    }

    protected void Reset()
    {
        _timer?.Dispose();
        _disposeCallback?.Invoke();
        
        _timer = null;
        _disposeCallback = null;
    }
}