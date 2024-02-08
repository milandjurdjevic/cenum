The source generator will scan the source code for classes marked with the `[Enumeration]` attribute and then generate
an `Enumerate()` method for them. The method will enumerate all `public static readonly` fields with the same type as
the marked class itself.

An enumeration class cannot be nested or static.

### Example

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
