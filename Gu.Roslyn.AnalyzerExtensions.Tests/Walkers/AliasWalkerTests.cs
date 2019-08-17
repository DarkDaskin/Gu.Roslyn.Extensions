namespace Gu.Roslyn.AnalyzerExtensions.Tests.Walkers
{
    using System.Linq;
    using System.Threading;
    using Microsoft.CodeAnalysis.CSharp;
    using NUnit.Framework;

    public static class AliasWalkerTests
    {
        [Test]
        public static void Borrow()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    using A = NUnit.Framework.Assert;

    public class C
    {
        public C()
        {
            A.AreEqual(1, 1);
        }
    }
}");
            using (var walker = AliasWalker.Borrow(tree))
            {
                CollectionAssert.AreEqual(new[] { "using A = NUnit.Framework.Assert;" }, walker.Aliases.Select(x => x.ToString()));
            }

            using (var walker = AliasWalker.Borrow(tree.GetRoot(CancellationToken.None)))
            {
                CollectionAssert.AreEqual(new[] { "using A = NUnit.Framework.Assert;" }, walker.Aliases.Select(x => x.ToString()));
            }
        }

        [Test]
        public static void TryGetForName()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    using A = NUnit.Framework.Assert;

    public class C
    {
        public C()
        {
            A.AreEqual(1, 1);
        }
    }
}");
            Assert.AreEqual(true, AliasWalker.TryGet(tree, "A", out var directive));
            Assert.AreEqual("using A = NUnit.Framework.Assert;", directive.ToString());
        }

        [Test]
        public static void TryGetForType()
        {
            var tree = CSharpSyntaxTree.ParseText(@"
namespace N
{
    using A = NUnit.Framework.Assert;

    public class C
    {
        public C()
        {
            A.AreEqual(1, 1);
        }
    }
}");
            Assert.AreEqual(true, AliasWalker.TryGet(tree, new QualifiedType("NUnit.Framework.Assert"), out var directive));
            Assert.AreEqual("using A = NUnit.Framework.Assert;", directive.ToString());

            Assert.AreEqual(false,                                AliasWalker.TryGet(tree, new QualifiedType("System.String"), out _));
        }
    }
}
