namespace Debouncer;

public interface IDebounce
{
    int Timeout { get; }

    void Cancel();
    
    void Flush();

    void Invoke();
}