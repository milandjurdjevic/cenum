using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cenum;

[Generator]
public class Generator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(GeneratePostInitOutput);
        var syntaxProvider = context.SyntaxProvider
            .ForAttributeWithMetadataName(Marker.Default.MetadataName, Filter, Transform)
            .Collect();
        context.RegisterSourceOutput(syntaxProvider, GenerateSourceOutput);
    }

    private static Enumeration Transform(GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        var symbol = (ITypeSymbol)context.TargetSymbol;
        return new Enumeration(symbol);
    }

    private static bool Filter(SyntaxNode node, CancellationToken token) =>
        node is ClassDeclarationSyntax classDeclarationSyntax &&
        classDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword) &&
        !classDeclarationSyntax.Modifiers.Any(SyntaxKind.StaticKeyword);

    private static void GeneratePostInitOutput(IncrementalGeneratorPostInitializationContext context) =>
        context.AddSource(Marker.Default.HintName, Marker.Default.ToString());

    private static void GenerateSourceOutput(SourceProductionContext context, ImmutableArray<Enumeration> sources)
    {
        foreach (var source in sources)
        {
            context.AddSource(source.HintName, source.ToString());
        }
    }
}