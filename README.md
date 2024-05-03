# Cenum [![latest version](https://img.shields.io/nuget/v/cenum)](https://www.nuget.org/packages/cenum)

Object-oriented alternative to C# enum - base for defining enumerations as classes with fields, properties and methods.

## Motivation

Instead of being limited to a fixed set of integral values, imagine if enums were a fixed set of
objects. They could have fields, properties and methods - just as other types do.
The only difference would be that there would only ever be one instance for each value.

## Usage

To lear more about the library usage, check the [documentation](src/Cenum/README.md#usage) link.

## Benchmark

_Enumerate with foreach loop over 10 enumeration values_

| Method    |     Mean |    Error |   StdDev |   Gen0 | Allocated |
|-----------|---------:|---------:|---------:|-------:|----------:|
| Enumerate | 46.17 ns | 0.474 ns | 0.443 ns | 0.0051 |      32 B |

### Legends

- Mean: Arithmetic mean of all measurements
- Error: Half of 99.9% confidence interval
- StdDev: Standard deviation of all measurements
- Gen0: GC Generation 0 collects per 1000 operations
- Allocated: Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
- 1 ns: 1 Nanosecond (0.000000001 sec)

### Device

- Chip: Apple M1 Pro
- Memory: 16 GB
- OS: Sonoma 14.1.2

## License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for more details.