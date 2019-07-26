namespace Gu.Roslyn.CodeFixExtensions.Tests
{
    using Gu.Roslyn.Asserts;
    using Microsoft.CodeAnalysis.CSharp;
    using NUnit.Framework;

    public partial class CodeStyleTests
    {
        public class UnderscoreFields
        {
            [Test]
            public void DefaultsToFalse()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    internal class C
    {
        internal C(int i, double d)
        {
        }

        internal void P()
        {
        }
    }
}");
                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree });
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenFieldIsNamedWithUnderscore()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        int _f;
        public int P => _f = 1;
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree });
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(true, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenPropertyIsAssignedWithThis()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        public C(int bar)
        {
            this.P = bar;
        }

        public int P { get; set; }
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree });
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenPropertyIsAssignedWithoutThis()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        public C(int p)
        {
            P = p;
        }

        public int P { get; set; }
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree });
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(true, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenPropertyIsAssignedInObjectInitializer()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        public int P { get; set; }

        public static C Create(int i) => new C { P = i };
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree });
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenObjectInitializerInCollectionInitializer()
            {
                var CTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    public class C
    {
        public int Value { get; set; }
    }
}");

                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    using System.Collections.Generic;

    public class C
    {
        public List<C> Items { get; } = new List<C>
        {
            new C { Value = 2 },
        };
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { CTree, syntaxTree });
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenFieldIsNotNamedWithUnderscore()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        int value;
        public int P()  => value = 1;
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree }, MetadataReferences.FromAttributes());
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void WhenUsingThis()
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        public int Value { get; private set; }

        public int P()  => this.Value = 1;
    }
}");

                var compilation = CSharpCompilation.Create("test", new[] { syntaxTree }, MetadataReferences.FromAttributes());
                var semanticModel = compilation.GetSemanticModel(syntaxTree);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }

            [Test]
            public void FiguresOutFromOtherClass()
            {
                var CCode = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        private int _f;

        public int P()  => _f = 1;
    }
}");

                var barCode = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class P
    {
    }
}");
                var compilation = CSharpCompilation.Create("test", new[] { CCode, barCode }, MetadataReferences.FromAttributes());
                Assert.AreEqual(2, compilation.SyntaxTrees.Length);
                foreach (var tree in compilation.SyntaxTrees)
                {
                    var semanticModel = compilation.GetSemanticModel(tree);
                    Assert.AreEqual(true, CodeStyle.UnderscoreFields(semanticModel));
                }
            }

            [Test]
            public void ChecksContainingClassFirst()
            {
                var CCode = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class C
    {
        private int _f;

        public int P()  => _f = 1;
    }
}");

                var barCode = CSharpSyntaxTree.ParseText(@"
namespace N
{
    class P
    {
        private int value;
    }
}");
                var compilation = CSharpCompilation.Create("test", new[] { CCode, barCode }, MetadataReferences.FromAttributes());
                var semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees[0]);
                Assert.AreEqual(true, CodeStyle.UnderscoreFields(semanticModel));

                semanticModel = compilation.GetSemanticModel(compilation.SyntaxTrees[1]);
                Assert.AreEqual(false, CodeStyle.UnderscoreFields(semanticModel));
            }
        }
    }
}
