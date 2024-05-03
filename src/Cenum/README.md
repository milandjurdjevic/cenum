# Cenum

Object-oriented alternative to C# enum - base for defining enumerations as classes with fields, properties and methods.

## Usage

Define an enumeration by creating a partial class and decorating it with the `Enumeration` attribute.
The class should have a public static readonly field for each value of the "enumeration".

Generated method `Enumerate` returns an `IEnumerable<T>` containing all values of the enumeration.

```csharp
[Enumeration]
public partial class Number
{
    public static readonly Number One = new(1);
    public static readonly Number Two = new(2);
    public static readonly Number Three = new(3);
    
    private readonly int _value;
    
    private Number(int value) => _value = value;
    
    public override string ToString() => _value.ToString();
}


foreach (var number in Number.Enumerate())
{
    Console.WriteLine(number.ToString());
}

// Output:
// 1
// 2
// 3
```