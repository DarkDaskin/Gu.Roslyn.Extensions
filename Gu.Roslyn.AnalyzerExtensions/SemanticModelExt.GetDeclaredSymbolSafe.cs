namespace Gu.Roslyn.AnalyzerExtensions
{
    using System.Threading;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helpers for getting declared symbol when not sure it is in the syntax tree of the semantic model.
    /// </summary>
    public static partial class SemanticModelExt
    {
        /// <summary>
        /// Try getting the <see cref="ITypeSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="TypeDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, TypeDeclarationSyntax node, CancellationToken cancellationToken, out ITypeSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IFieldSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="FieldDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, FieldDeclarationSyntax node, CancellationToken cancellationToken, out IFieldSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IMethodSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="ConstructorDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, ConstructorDeclarationSyntax node, CancellationToken cancellationToken, out IMethodSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IEventSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="EventDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, EventDeclarationSyntax node, CancellationToken cancellationToken, out IEventSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IEventSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="EventFieldDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, EventFieldDeclarationSyntax node, CancellationToken cancellationToken, out IEventSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IPropertySymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="PropertyDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, PropertyDeclarationSyntax node, CancellationToken cancellationToken, out IPropertySymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IPropertySymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="IndexerDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, IndexerDeclarationSyntax node, CancellationToken cancellationToken, out IPropertySymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IMethodSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="MethodDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, MethodDeclarationSyntax node, CancellationToken cancellationToken, out IMethodSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="IParameterSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="ParameterSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, ParameterSyntax node, CancellationToken cancellationToken, out IParameterSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="ILocalSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="VariableDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, VariableDeclarationSyntax node, CancellationToken cancellationToken, out ILocalSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Try getting the <see cref="ILocalSymbol"/> for the node.
        /// Gets the semantic model for the tree if the node is not in the tree corresponding to <paramref name="semanticModel"/>.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="VariableDesignationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <param name="symbol">The symbol if found.</param>
        /// <returns>True if a symbol was found.</returns>
        public static bool TryGetSymbol(this SemanticModel semanticModel, VariableDesignationSyntax node, CancellationToken cancellationToken, out ILocalSymbol symbol)
        {
            symbol = GetDeclaredSymbolSafe(semanticModel, node, cancellationToken);
            return symbol != null;
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="TypeDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="ITypeSymbol"/> or null.</returns>
        public static ITypeSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, TypeDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return (ITypeSymbol)semanticModel.SemanticModelFor(node)
                                             ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="SyntaxNode"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IFieldSymbol"/> or null.</returns>
        public static IFieldSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, FieldDeclarationSyntax node, CancellationToken cancellationToken)
        {
            if (node?.Declaration is VariableDeclarationSyntax variableDeclaration &&
                variableDeclaration.Variables.TrySingle(out var variable))
            {
                return (IFieldSymbol)semanticModel.SemanticModelFor(node)
                                                  ?.GetDeclaredSymbol(variable, cancellationToken);
            }

            return null;
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="ConstructorDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IMethodSymbol"/> or null.</returns>
        public static IMethodSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, ConstructorDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return (IMethodSymbol)semanticModel.SemanticModelFor(node)
                                               ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="EventDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IEventSymbol"/> or null.</returns>
        public static IEventSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, EventDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return (IEventSymbol)semanticModel.SemanticModelFor(node)
                                                 ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="EventFieldDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IEventSymbol"/> or null.</returns>
        public static IEventSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, EventFieldDeclarationSyntax node, CancellationToken cancellationToken)
        {
            if (node?.Declaration is VariableDeclarationSyntax variableDeclaration &&
                variableDeclaration.Variables.TrySingle(out var variable))
            {
                return semanticModel.SemanticModelFor(node)
                                    ?.GetDeclaredSymbol(variable, cancellationToken) as IEventSymbol;
            }

            return null;
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="PropertyDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IPropertySymbol"/> or null.</returns>
        public static IPropertySymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, PropertyDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return (IPropertySymbol)semanticModel.SemanticModelFor(node)
                                                 ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="IndexerDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IPropertySymbol"/> or null.</returns>
        public static IPropertySymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, IndexerDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return (IPropertySymbol)semanticModel.SemanticModelFor(node)
                                                 ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="BasePropertyDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="ISymbol"/> or null.</returns>
        public static ISymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, BasePropertyDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return semanticModel.SemanticModelFor(node)
                                ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="MethodDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IMethodSymbol"/> or null.</returns>
        public static IMethodSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, MethodDeclarationSyntax node, CancellationToken cancellationToken)
        {
            return (IMethodSymbol)semanticModel.SemanticModelFor(node)
                                               ?.GetDeclaredSymbol(node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="ParameterSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="IParameterSymbol"/> or null.</returns>
        public static IParameterSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, ParameterSyntax node, CancellationToken cancellationToken)
        {
            return (IParameterSymbol)GetDeclaredSymbolSafe(semanticModel, (SyntaxNode)node, cancellationToken);
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="VariableDeclarationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="ILocalSymbol"/> or null.</returns>
        public static ILocalSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, VariableDeclarationSyntax node, CancellationToken cancellationToken)
        {
            if (node?.Variables == null)
            {
                return null;
            }

            if (node.Variables.TrySingle(out var variable))
            {
                return (ILocalSymbol)semanticModel.SemanticModelFor(node)
                                                  ?.GetDeclaredSymbol(variable, cancellationToken);
            }

            return null;
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="VariableDesignationSyntax"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="ILocalSymbol"/> or null.</returns>
        public static ILocalSymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, VariableDesignationSyntax node, CancellationToken cancellationToken)
        {
            if (node is null)
            {
                return null;
            }

            if (node is SingleVariableDesignationSyntax singleVariable)
            {
                return (ILocalSymbol)semanticModel.SemanticModelFor(node)
                                                   ?.GetDeclaredSymbol(singleVariable, cancellationToken);
            }

            return null;
        }

        /// <summary>
        /// Same as SemanticModel.GetDeclaredSymbol but works when <paramref name="node"/> is not in the syntax tree.
        /// </summary>
        /// <param name="semanticModel">The <see cref="SemanticModel"/>.</param>
        /// <param name="node">The <see cref="SyntaxNode"/>.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/>.</param>
        /// <returns>An <see cref="ISymbol"/> or null.</returns>
        public static ISymbol GetDeclaredSymbolSafe(this SemanticModel semanticModel, SyntaxNode node, CancellationToken cancellationToken)
        {
            if (node is FieldDeclarationSyntax fieldDeclaration)
            {
                return GetDeclaredSymbolSafe(semanticModel, fieldDeclaration, cancellationToken);
            }

            return semanticModel.SemanticModelFor(node)
                                ?.GetDeclaredSymbol(node, cancellationToken);
        }
    }
}
