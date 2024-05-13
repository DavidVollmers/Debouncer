namespace Debouncer;

public interface IDebounce
{
    int Timeout { get; }
    
    void Flush(CancellationToken cancellationToken = default);

    void Invoke(CancellationToken cancellationToken = default);
}