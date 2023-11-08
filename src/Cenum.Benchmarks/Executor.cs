using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Attributes;

namespace Cenum.Benchmarks;

[MemoryDiagnoser]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class Executor
{
    [Benchmark]
    public void EnumerateForEach()
    {
        foreach (Number? _ in Number.Enumerate())
        {
            // Do nothing.
        }
    }
}