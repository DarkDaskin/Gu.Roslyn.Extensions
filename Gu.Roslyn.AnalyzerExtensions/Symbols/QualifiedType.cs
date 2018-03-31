namespace Gu.Roslyn.AnalyzerExtensions
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    [global::System.Diagnostics.DebuggerDisplay("{this.FullName}")]
    public class QualifiedType
    {
        internal readonly string FullName;
        internal readonly NamespaceParts Namespace;
        internal readonly string Type;
        private readonly string @alias;

        public QualifiedType(string qualifiedName, string alias = null)
            : this(qualifiedName, NamespaceParts.Create(qualifiedName), qualifiedName.Substring(qualifiedName.LastIndexOf('.') + 1), alias)
        {
        }

        private QualifiedType(string fullName, NamespaceParts @namespace, string type, string alias = null)
        {
            this.FullName = fullName;
            this.Namespace = @namespace;
            this.Type = type;
            this.alias = alias;
        }

        public static bool operator ==(ITypeSymbol left, QualifiedType right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            return NameEquals(left.MetadataName, right) &&
                   left.ContainingNamespace == right.Namespace;
        }

        public static bool operator !=(ITypeSymbol left, QualifiedType right) => !(left == right);

        public static bool operator ==(BaseTypeSyntax left, QualifiedType right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            return left.Type == right;
        }

        public static bool operator !=(BaseTypeSyntax left, QualifiedType right) => !(left == right);

        public static bool operator ==(TypeSyntax left, QualifiedType right)
        {
            if (left == null && right == null)
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            if (left is SimpleNameSyntax simple)
            {
                return NameEquals(simple.Identifier.ValueText, right);
            }

            if (left is QualifiedNameSyntax qualified)
            {
                return NameEquals(qualified.Right.Identifier.ValueText, right) &&
                       right.Namespace.Matches(qualified.Left);
            }

            return false;
        }

        public static bool operator !=(TypeSyntax left, QualifiedType right) => !(left == right);

        private static bool NameEquals(string left, QualifiedType right)
        {
            return left == right.Type ||
                   (right.alias != null && left == right.alias);
        }

        public static class System
        {
            public static readonly QualifiedType Void = new QualifiedType("System.Void", "void");
            public static readonly QualifiedType Object = new QualifiedType("System.Object", "object");
            public static readonly QualifiedType Boolean = new QualifiedType("System.Boolean", "bool");
            public static readonly QualifiedType String = new QualifiedType("System.String", "string");
        }
    }
}
