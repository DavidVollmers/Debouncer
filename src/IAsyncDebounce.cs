namespace Debouncer;

public interface IAsyncDebounce
{
    int Timeout { get; }
    
    Task FlushAsync(CancellationToken cancellationToken = default);

    Task InvokeAsync(CancellationToken cancellationToken = default);
}