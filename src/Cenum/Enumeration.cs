using System.CodeDom.Compiler;

using Microsoft.CodeAnalysis;

namespace Cenum;

internal class Enumeration
{
    private readonly ITypeSymbol _symbol;
    public Enumeration(ITypeSymbol symbol) => _symbol = symbol;

    public string HintName => _symbol.ContainingNamespace.IsGlobalNamespace
        ? $"{_symbol.Name}.g.cs"
        : $"{_symbol.ContainingNamespace.ToDisplayString()}.{_symbol.Name}.g.cs";

    private static IEnumerable<string> MembersOf(INamespaceOrTypeSymbol type) =>
        type.GetMembers()
            .OfType<IFieldSymbol>()
            .Where(field => IsMember(type, field))
            .Select(field => field.Name);

    private static bool IsMember(ISymbol type, IFieldSymbol field) =>
        field is { IsStatic: true, DeclaredAccessibility: Accessibility.Public, IsReadOnly: true } &&
        SymbolEqualityComparer.Default.Equals(field.Type, type);

    public override string ToString()
    {
        using var writer = new IndentedTextWriter(new StringWriter());
        writer.WriteLine("// <auto-generated />");

        if (!_symbol.ContainingNamespace.IsGlobalNamespace)
        {
            writer.WriteLine($"namespace {_symbol.ContainingNamespace.ToDisplayString()}");
            writer.WriteLine('{');
            writer.Indent++;
        }

        writer.WriteLine($"{_symbol.DeclaredAccessibility.ToString().ToLowerInvariant()} partial class {_symbol.Name}");
        writer.WriteLine('{');
        writer.Indent++;
        writer.WriteLine($"public static global::System.Collections.Generic.IEnumerable<{_symbol.Name}> Enumerate()");
        writer.WriteLine('{');
        writer.Indent++;

        var members = MembersOf(_symbol).ToList();
        if (members.Count > 0)
        {
            foreach (var member in members)
            {
                writer.WriteLine($"yield return {member};");
            }
        }
        else
        {
            writer.WriteLine($"return global::System.Linq.Enumerable.Empty<{_symbol.Name}>()");
        }

        writer.Indent--;
        writer.WriteLine('}');
        writer.Indent--;
        writer.WriteLine('}');

        if (_symbol.ContainingNamespace.IsGlobalNamespace)
        {
            return writer.InnerWriter.ToString();
        }

        writer.Indent--;
        writer.WriteLine('}');
        return writer.InnerWriter.ToString();
    }
}