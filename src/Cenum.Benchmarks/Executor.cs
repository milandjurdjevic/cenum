using System.Diagnostics.CodeAnalysis;

using BenchmarkDotNet.Attributes;

namespace Cenum.Benchmarks;

[MemoryDiagnoser]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class Executor
{
    [Benchmark]
    public void Enumerate()
    {
        foreach (Number _ in Number.Enumerate())
        {
            // Iterate over the enumeration just for the evaluation purpose.
        }
    }
}