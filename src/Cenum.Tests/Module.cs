using System.Runtime.CompilerServices;

namespace Cenum.Tests;

public static class Module
{
    [ModuleInitializer]
    public static void Initialize() => VerifySourceGenerators.Initialize();
}