namespace Debouncer.Tests;

public class AsyncDebounceTests
{
    [Fact]
    public async Task Debounce_Action_InvokesOnce()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => Task.FromResult(invocationCount++), timeout);

        // Act
        var task1 = debounce.InvokeAsync();
        var task2 = debounce.InvokeAsync();
        var task3 = debounce.InvokeAsync();
        var task4 = debounce.InvokeAsync();
        var task5 = debounce.InvokeAsync();

        // Assert
        Assert.Equal(0, invocationCount);

        Assert.True(task1.IsCanceled);
        Assert.True(task2.IsCanceled);
        Assert.True(task3.IsCanceled);
        Assert.True(task4.IsCanceled);
        Assert.False(task5.IsCompleted);

        await task5;

        Assert.Equal(1, invocationCount);
        
        Assert.True(task5.IsCompletedSuccessfully);
        
        await Task.Delay(timeout * 2);
        
        Assert.Equal(1, invocationCount);
    }
    
    [Fact]
    public async Task Debounce_Action_Cancel()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => Task.FromResult(invocationCount++), timeout);

        // Act
        var task1 = debounce.InvokeAsync();
        var task2 = debounce.InvokeAsync();
        var task3 = debounce.InvokeAsync();
        var task4 = debounce.InvokeAsync();
        var task5 = debounce.InvokeAsync();

        // Assert
        Assert.Equal(0, invocationCount);
        
        Assert.True(task1.IsCanceled);
        Assert.True(task2.IsCanceled);
        Assert.True(task3.IsCanceled);
        Assert.True(task4.IsCanceled);
        Assert.False(task5.IsCompleted);

        debounce.Cancel();
        
        await Task.Delay(timeout * 2);
        
        Assert.Equal(0, invocationCount);
        
        Assert.True(task5.IsCanceled);
    }
    
    [Fact]
    public async Task Debounce_Action_Flush()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => Task.FromResult(invocationCount++), timeout);

        // Act
        var task1 = debounce.InvokeAsync();
        var task2 = debounce.InvokeAsync();
        var task3 = debounce.InvokeAsync();
        var task4 = debounce.InvokeAsync();
        var task5 = debounce.InvokeAsync();

        // Assert
        Assert.Equal(0, invocationCount);
        
        Assert.True(task1.IsCanceled);
        Assert.True(task2.IsCanceled);
        Assert.True(task3.IsCanceled);
        Assert.True(task4.IsCanceled);
        Assert.False(task5.IsCompleted);

        await debounce.FlushAsync();
        
        Assert.Equal(1, invocationCount);
        
        Assert.True(task5.IsCanceled);
        
        await Task.Delay(timeout * 2);
        
        Assert.Equal(1, invocationCount);
    }
    
    [Fact]
    public async Task Debounce_Action_InvokeTwice()
    {
        // Arrange
        var invocationCount = 0;
        const int timeout = 100;
        var debounce = Debouncer.Debounce(() => Task.FromResult(invocationCount++), timeout);

        // Act
        var task1 = debounce.InvokeAsync();
        var task2 = debounce.InvokeAsync();
        var task3 = debounce.InvokeAsync();
        var task4 = debounce.InvokeAsync();
        var task5 = debounce.InvokeAsync();

        // Assert
        Assert.Equal(0, invocationCount);

        Assert.True(task1.IsCanceled);
        Assert.True(task2.IsCanceled);
        Assert.True(task3.IsCanceled);
        Assert.True(task4.IsCanceled);
        Assert.False(task5.IsCompleted);

        await task5;

        Assert.Equal(1, invocationCount);
        
        Assert.True(task5.IsCompletedSuccessfully);
        
        await Task.Delay(timeout * 2);
        
        Assert.Equal(1, invocationCount);
        
        // Act
        var task6 = debounce.InvokeAsync();
        
        // Assert
        Assert.Equal(1, invocationCount);

        await task6;

        Assert.Equal(2, invocationCount);
        
        Assert.True(task6.IsCompletedSuccessfully);
        
        await Task.Delay(timeout * 2);
        
        Assert.Equal(2, invocationCount);
    }
}