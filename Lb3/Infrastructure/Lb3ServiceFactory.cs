using Lpnu.Lb3.Core.Services;
using Lpnu.Lb3.Infrastructure.Services;

namespace Lpnu.Lb3.Infrastructure;

/// <summary>
/// Factory provider creating instances of Lab 3 calculation contracts.
/// </summary>
public static class Lb3ServiceFactory
{
    /// <summary>
    /// Creates a new instance of <see cref="ITask31Calculator"/>.
    /// </summary>
    public static ITask31Calculator CreateTask31Calculator() => new Task31Calculator();

    /// <summary>
    /// Creates a new instance of <see cref="ITask32Calculator"/>.
    /// </summary>
    public static ITask32Calculator CreateTask32Calculator() => new Task32Calculator();

    /// <summary>
    /// Creates a new instance of <see cref="ITask33Calculator"/>.
    /// </summary>
    public static ITask33Calculator CreateTask33Calculator() => new Task33Calculator();

    /// <summary>
    /// Creates a new instance of <see cref="ITask34Calculator"/>.
    /// </summary>
    public static ITask34Calculator CreateTask34Calculator() => new Task34Calculator();
}
