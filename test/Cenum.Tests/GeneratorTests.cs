using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Cenum.Tests;

[UsesVerify]
public class GeneratorTests
{
    [Fact]
    public Task GeneratesExpectedSourceCode()
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(
            """
            using Cenum;

            namespace EnumerationNamespace
            {
               [Enumeration]
               public partial class EnumerationClass
               {
                   public static readonly EnumerationClass One = new();
                   public static readonly EnumerationClass Two = new();
                   public static readonly EnumerationClass Three = new();
               }
               
                [Enumeration]
                internal partial class InternalEnumerationClass
                {
                    public static readonly InternalEnumerationClass One = new();
                    public static readonly InternalEnumerationClass Two = new();
                    public static readonly InternalEnumerationClass Three = new();
                }
               
                [Enumeration]
                public partial class EmptyEnumerationClass { }
                
                [Enumeration]
                public class NonPartialEnumerationClass
                {
                    public static readonly NonPartialEnumerationClass One = new();
                }
                
                public class EnumerationWrapperClass
                {
                    [Enumeration]
                    public partial class EnumerationNestedClass
                    {
                        public static readonly EnumerationInnerClass One = new();
                    }
                }
            }

            [Enumeration]
            public partial class GlobalEnumerationClass
            {
                public static readonly GlobalEnumerationClass One = new();
            }
            
            [Enumeration]
            internal partial class InternalGlobalEnumerationClass
            {
                public static readonly InternalGlobalEnumerationClass One = new();
                public static readonly InternalGlobalEnumerationClass Two = new();
                public static readonly InternalGlobalEnumerationClass Three = new();
            }
            """);


        var compilation = CSharpCompilation.Create(nameof(GeneratorTests),
            new[] { syntaxTree },
            new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

        var generator = new Generator();
        var driver = CSharpGeneratorDriver.Create(generator).RunGenerators(compilation);

        return Verify(driver);
    }
}