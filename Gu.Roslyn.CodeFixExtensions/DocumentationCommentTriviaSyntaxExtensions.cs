namespace Gu.Roslyn.CodeFixExtensions
{
    using System;
    using System.Linq;
    using Gu.Roslyn.AnalyzerExtensions;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Helper methods for <see cref="DocumentationCommentTriviaSyntax"/>
    /// </summary>
    public static class DocumentationCommentTriviaSyntaxExtensions
    {
        /// <summary>
        /// Add a summary element to <paramref name="comment"/>
        /// Replace if a summary element exists.
        /// </summary>
        /// <param name="comment">The <see cref="DocumentationCommentTriviaSyntax"/></param>
        /// <param name="text"> The text to add inside the summary element.</param>
        /// <returns><paramref name="comment"/> with summary.</returns>
        public static DocumentationCommentTriviaSyntax WithSummaryText(this DocumentationCommentTriviaSyntax comment, string text)
        {
            return comment.WithSummary(Parse.XmlElementSyntax(CreateElementXml(text, "summary"), comment.LeadingWhitespace()));
        }

        /// <summary>
        /// Add <paramref name="summary"/> element to <paramref name="comment"/>
        /// Replace if a summary element exists.
        /// </summary>
        /// <param name="comment">The <see cref="DocumentationCommentTriviaSyntax"/></param>
        /// <param name="summary"> The <see cref="XmlElementSyntax"/>.</param>
        /// <returns><paramref name="comment"/> with <paramref name="summary"/>.</returns>
        public static DocumentationCommentTriviaSyntax WithSummary(this DocumentationCommentTriviaSyntax comment, XmlElementSyntax summary)
        {
            if (comment.TryGetSummary(out var old))
            {
                return comment.ReplaceNode(old, summary);
            }

            if (comment.Content.TryFirstOfType(out XmlElementSyntax existing))
            {
                return comment.InsertBefore(existing, summary);
            }

            return comment.WithContent(comment.Content.Add(summary));
        }

        /// <summary>
        /// Add a summary element to <paramref name="comment"/>
        /// Replace if a summary element exists.
        /// If the comment is attached to a method the param element is inserted at the correct position.
        /// </summary>
        /// <param name="comment">The <see cref="DocumentationCommentTriviaSyntax"/></param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <param name="text"> The text to add inside the summary element.</param>
        /// <returns><paramref name="comment"/> with summary.</returns>
        public static DocumentationCommentTriviaSyntax WithParamText(this DocumentationCommentTriviaSyntax comment, string parameterName, string text)
        {
            return comment.WithParam(Parse.XmlElementSyntax(CreateElementXml(text, "param", parameterName), comment.LeadingWhitespace()));
        }

        /// <summary>
        /// Add <paramref name="param"/> element to <paramref name="comment"/>
        /// Replace if a summary element exists.
        /// If the comment is attached to a method the param element is inserted at the correct position.
        /// </summary>
        /// <param name="comment">The <see cref="DocumentationCommentTriviaSyntax"/></param>
        /// <param name="param"> The <see cref="XmlElementSyntax"/>.</param>
        /// <returns><paramref name="comment"/> with <paramref name="param"/>.</returns>
        public static DocumentationCommentTriviaSyntax WithParam(this DocumentationCommentTriviaSyntax comment, XmlElementSyntax param)
        {
            if (param.TryGetNameAttribute(out var attribute) &&
                attribute.Identifier is IdentifierNameSyntax identifierName)
            {
                if (comment.TryGetParam(identifierName.Identifier.ValueText, out var old))
                {
                    return comment.ReplaceNode(old, param);
                }

                if (TryGetPositionFromParam(out var before, out var after))
                {
                    if (after != null)
                    {
                        return comment.InsertBefore(after, param);
                    }

                    return comment.InsertAfter(before, param);
                }

                foreach (var node in comment.Content)
                {
                    if (node is XmlElementSyntax e &&
                        (e.HasLocalName("returns") ||
                         e.HasLocalName("exception")))
                    {
                        return comment.InsertBefore(e, param);
                    }
                }

                return comment.InsertAfter(comment.Content.OfType<XmlElementSyntax>().Last(), param);
            }

            throw new ArgumentException("Element does not have a name attribute.", nameof(param));

            bool TryGetPositionFromParam(out XmlElementSyntax before, out XmlElementSyntax after)
            {
                before = null;
                after = null;
                if (comment.TryFirstAncestor(out MethodDeclarationSyntax method) &&
                    method.TryFindParameter(identifierName.Identifier.ValueText, out var parameter) &&
                    method.ParameterList.Parameters.IndexOf(parameter) is var ordinal &&
                    ordinal >= 0)
                {
                    foreach (var node in comment.Content)
                    {
                        if (node is XmlElementSyntax e &&
                            e.HasLocalName("param") &&
                            e.TryGetNameAttribute(out var nameAttribute) &&
                            method.TryFindParameter(nameAttribute.Identifier.Identifier.ValueText, out var other))
                        {
                            before = e;
                            if (ordinal < method.ParameterList.Parameters.IndexOf(other))
                            {
                                after = e;
                                return true;
                            }
                        }
                    }
                }

                return before != null;
            }
        }

        /// <summary>
        /// Add the element and newline and trivia.
        /// </summary>
        /// <param name="comment">The <see cref="DocumentationCommentTriviaSyntax"/></param>
        /// <param name="existing">The element already in comment.Content</param>
        /// <param name="element">The element to add.</param>
        /// <returns>A <see cref="DocumentationCommentTriviaSyntax"/> withe <paramref name="element"/> added</returns>
        public static DocumentationCommentTriviaSyntax InsertBefore(this DocumentationCommentTriviaSyntax comment, XmlElementSyntax existing, XmlElementSyntax element)
        {
            return comment.WithContent(comment.Content.InsertRange(
                comment.Content.IndexOf(existing),
                new XmlNodeSyntax[]
                {
                    element,
                    XmlNewLine(comment)
                }));
        }

        /// <summary>
        /// Add the element and newline and trivia.
        /// </summary>
        /// <param name="comment">The <see cref="DocumentationCommentTriviaSyntax"/></param>
        /// <param name="existing">The element already in comment.Content</param>
        /// <param name="element">The element to add.</param>
        /// <returns>A <see cref="DocumentationCommentTriviaSyntax"/> withe <paramref name="element"/> added</returns>
        public static DocumentationCommentTriviaSyntax InsertAfter(this DocumentationCommentTriviaSyntax comment, XmlElementSyntax existing, XmlElementSyntax element)
        {
            return comment.WithContent(comment.Content.InsertRange(
                comment.Content.IndexOf(existing) + 1,
                new XmlNodeSyntax[]
                {
                    XmlNewLine(comment),
                    element,
                }));
        }

        private static XmlTextSyntax XmlNewLine(DocumentationCommentTriviaSyntax comment)
        {
            return SyntaxFactory.XmlText(
                    SyntaxFactory.Token(SyntaxTriviaList.Empty, SyntaxKind.XmlTextLiteralNewLineToken,
                        Environment.NewLine, Environment.NewLine, SyntaxTriviaList.Empty),
                    SyntaxFactory.Token(
                        leading: SyntaxFactory.TriviaList(SyntaxFactory.SyntaxTrivia(SyntaxKind.DocumentationCommentExteriorTrivia, $"{comment.LeadingWhitespace()}///")),
                        kind: SyntaxKind.XmlTextLiteralToken,
                        text: " ",
                        valueText: " ",
                        trailing: SyntaxTriviaList.Empty))
                .WithLeadingTrivia(SyntaxTriviaList.Empty);
        }

        private static string CreateElementXml(string content, string localName)
        {
            if (content.IndexOf('\n') < 0)
            {
                return $"<{localName}>{content}</{localName}>";
            }

            return StringBuilderPool.Borrow()
                                    .AppendLine($"<{localName}>")
                                    .Append(content)
                                    .AppendLine()
                                    .AppendLine($"</{localName}>")
                                    .Return();
        }

        private static string CreateElementXml(string content, string localName, string nameArgumentValue)
        {
            if (content.IndexOf('\n') < 0)
            {
                return $"<{localName} name=\"{nameArgumentValue}\">{content}</{localName}>";
            }

            return StringBuilderPool.Borrow()
                                    .AppendLine($"<{localName} name=\"{nameArgumentValue}\">")
                                    .Append(content)
                                    .AppendLine()
                                    .AppendLine($"</{localName}>")
                                    .Return();
        }

        private static string LeadingWhitespace(this DocumentationCommentTriviaSyntax comment)
        {
            return comment.ParentTrivia.Token.LeadingTrivia.TryFirst(x => x.IsKind(SyntaxKind.WhitespaceTrivia), out var whitespaceTrivia)
                ? whitespaceTrivia.ToString()
                : "        ";
        }
    }
}
