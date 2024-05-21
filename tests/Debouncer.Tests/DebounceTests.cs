namespace Debouncer.Tests;

public class DebounceTests
{
    [Fact]
    public void Debounce_Action_InvokesOnce()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => invocationCount++, timeout);

        // Act
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();

        // Assert
        Assert.Equal(0, invocationCount);

        Task.Delay(timeout * 2).Wait();

        Assert.Equal(1, invocationCount);
    }

    [Fact]
    public void Debounce_Action_Cancel()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => invocationCount++, timeout);

        // Act
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();

        // Assert
        Assert.Equal(0, invocationCount);

        debounce.Cancel();

        Task.Delay(timeout * 2).Wait();
        
        Assert.Equal(0, invocationCount);
    }
    
    [Fact]
    public void Debounce_Action_Flush()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => invocationCount++, timeout);

        // Act
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();
        debounce.Invoke();

        // Assert
        Assert.Equal(0, invocationCount);

        debounce.Flush();
        
        Assert.Equal(1, invocationCount);

        Task.Delay(timeout * 2).Wait();
        
        Assert.Equal(1, invocationCount);
    }
}