namespace Gu.Roslyn.AnalyzerExtensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper methods for finding the declaration.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class ISymbolExt
    {
        /// <summary>
        /// Try to get the single declaration of a property.
        /// </summary>
        /// <param name="field">The <see cref="IPropertySymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration(this IFieldSymbol field, CancellationToken cancellationToken, [NotNullWhen(true)]out FieldDeclarationSyntax? declaration)
        {
            declaration = null;
            if (field != null &&
                field.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken).FirstAncestorOrSelf<FieldDeclarationSyntax>();
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of a property.
        /// </summary>
        /// <param name="property">The <see cref="IPropertySymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration(this IPropertySymbol property, CancellationToken cancellationToken, [NotNullWhen(true)]out BasePropertyDeclarationSyntax? declaration)
        {
            declaration = null;
            if (property != null &&
                property.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken) as BasePropertyDeclarationSyntax;
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of a property.
        /// </summary>
        /// <param name="symbol">The <see cref="IEventSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration(this IEventSymbol symbol, CancellationToken cancellationToken, [NotNullWhen(true)]out MemberDeclarationSyntax? declaration)
        {
            declaration = null;
            if (symbol != null &&
                symbol.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken) as MemberDeclarationSyntax;
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of an event.
        /// </summary>
        /// <param name="symbol">The <see cref="IEventSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleEventDeclaration(this IEventSymbol symbol, CancellationToken cancellationToken, [NotNullWhen(true)]out EventDeclarationSyntax? declaration)
        {
            declaration = null;
            if (symbol != null &&
                symbol.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken) as EventDeclarationSyntax;
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of an event.
        /// </summary>
        /// <param name="symbol">The <see cref="IEventSymbol"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleEventFieldDeclaration(this IEventSymbol symbol, CancellationToken cancellationToken, [NotNullWhen(true)]out EventFieldDeclarationSyntax? declaration)
        {
            declaration = null;
            if (symbol != null &&
                symbol.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken) as EventFieldDeclarationSyntax;
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of a method.
        /// </summary>
        /// <param name="method">The <see cref="IMethodSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleMethodDeclaration(this IMethodSymbol method, CancellationToken cancellationToken, [NotNullWhen(true)]out MethodDeclarationSyntax? declaration)
        {
            return TrySingleDeclaration(method, cancellationToken, out declaration);
        }

        /// <summary>
        /// Try to get the single declaration of a method.
        /// </summary>
        /// <param name="method">The <see cref="IMethodSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleAccessorDeclaration(this IMethodSymbol method, CancellationToken cancellationToken, [NotNullWhen(true)]out AccessorDeclarationSyntax? declaration)
        {
            return TrySingleDeclaration(method, cancellationToken, out declaration);
        }

        /// <summary>
        /// Try to get the single declaration of a method.
        /// </summary>
        /// <typeparam name="T">Either BaseMethodDeclaration or AccessorDeclaration.</typeparam>
        /// <param name="method">The <see cref="IMethodSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration<T>(this IMethodSymbol method, CancellationToken cancellationToken, [NotNullWhen(true)]out T? declaration)
            where T : SyntaxNode
        {
            declaration = null;
            if (method != null &&
                method.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken) as T;
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of a property.
        /// </summary>
        /// <param name="parameter">The <see cref="IParameterSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration(this IParameterSymbol parameter, CancellationToken cancellationToken, [NotNullWhen(true)]out ParameterSyntax? declaration)
        {
            declaration = null;
            if (parameter != null &&
                parameter.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken) as ParameterSyntax;
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of a local.
        /// A local can either be declared using localdeclaration or inline out.
        /// </summary>
        /// <param name="local">The <see cref="ILocalSymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration(this ILocalSymbol local, CancellationToken cancellationToken, [NotNullWhen(true)]out SyntaxNode? declaration)
        {
            declaration = null;
            if (local != null &&
                local.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken);
            }

            return declaration != null;
        }

        /// <summary>
        /// Try to get the single declaration of a local.
        /// A local can either be declared using localdeclaration or inline out.
        /// </summary>
        /// <typeparam name="T">The expected node type.</typeparam>
        /// <param name="symbol">The <see cref="ISymbol"/>. </param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="declaration">The declaration.</param>
        /// <returns>True if one declaration was found.</returns>
        public static bool TrySingleDeclaration<T>(this ISymbol symbol, CancellationToken cancellationToken, [NotNullWhen(true)]out T? declaration)
            where T : SyntaxNode
        {
            declaration = null;
            if (symbol is null)
            {
                return false;
            }

            if (symbol.DeclaringSyntaxReferences.TrySingle(out var reference))
            {
                declaration = reference.GetSyntax(cancellationToken).FirstAncestorOrSelf<T>();
                return declaration != null;
            }

            return false;
        }
    }
}
