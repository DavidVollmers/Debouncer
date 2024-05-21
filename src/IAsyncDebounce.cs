namespace Debouncer;

public interface IAsyncDebounce
{
    int Timeout { get; }
    
    void Cancel();
    
    Task FlushAsync();

    Task InvokeAsync();
}