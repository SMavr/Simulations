using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyMapper
{
    [Generator]
    public class HelloWorldGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // Optional: Register for syntax notifications
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var source = @"
using System;

namespace Generated
{
    public static class HelloWorld
    {
        public static void SayHello() => Console.WriteLine(""Hello from generated code!"");
    }
}
";
            context.AddSource("HelloWorld.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}
