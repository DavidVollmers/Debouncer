[![](https://img.shields.io/github/v/release/DavidVollmers/Debouncer)](https://github.com/DavidVollmers/Debouncer/releases)
[![](https://img.shields.io/nuget/dt/Debouncer)](https://www.nuget.org/packages/Debouncer)
[![](https://img.shields.io/github/license/DavidVollmers/Debouncer)](https://github.com/DavidVollmers/Debouncer/blob/master/LICENSE.txt)

# Debouncer

> Debounce your code!

## What and Why?

Debouncing is a programming practice used to ensure that time-consuming tasks do not fire so often, that it stalls the
performance of the web page. In other words, it limits the rate at which a function gets invoked.

A real-life example of debouncing is the search functionality on Google. When you type something in the search bar, it
doesn't start searching immediately. It waits for you to stop typing for a moment, and then it starts searching.

## Installation

```shell
dotnet add package Debouncer
```

Visit [nuget.org](https://www.nuget.org/packages/Debouncer) for more information.

## Usage

```csharp
using Debouncer;

var debouncer = Debouncer.Debounce(() => {
    // Your code here
}, 1000);

debouncer.Invoke();
debouncer.Invoke();
debouncer.Invoke();

// After 1000ms, the code will be executed only once
```

---

## License

This project is licensed under the [MIT](LICENSE.txt) license.
