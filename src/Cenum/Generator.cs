using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Cenum;

[Generator(LanguageNames.CSharp)]
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

    private static Enumeration Transform(GeneratorAttributeSyntaxContext context, CancellationToken token) =>
        new((ITypeSymbol)context.TargetSymbol);

    private static bool Filter(SyntaxNode node, CancellationToken token) =>
        node is ClassDeclarationSyntax classDeclaration &&
        classDeclaration.Modifiers.Any(SyntaxKind.PartialKeyword) &&
        !classDeclaration.Modifiers.Any(SyntaxKind.StaticKeyword) &&
        classDeclaration.Parent is not ClassDeclarationSyntax;

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