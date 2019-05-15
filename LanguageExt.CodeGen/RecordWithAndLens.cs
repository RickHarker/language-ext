﻿using CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LanguageExt.CodeGen
{
    /// <summary>
    /// Provides a With function and lens fields for record types
    /// </summary>
    public class RecordWithAndLensGenerator : ICodeGenerator
    {
        /// <summary>
        /// Provides a With function and lens fields for record types
        /// </summary>
        public RecordWithAndLensGenerator(AttributeData attributeData)
        {
        }

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();

            // Our generator is applied to any class that our attribute is applied to.
            var applyToClass = (ClassDeclarationSyntax)context.ProcessingNode;

            var classModifiers = SyntaxFactory.TokenList(
                    Enumerable.Concat(
                        applyToClass.Modifiers
                                    .Where(t => !t.IsKind(SyntaxKind.PartialKeyword)).AsEnumerable(),
                        new[] { SyntaxFactory.Token(SyntaxKind.PartialKeyword) }));

            // Apply a suffix to the name of a copy of the class.
            var partialClass = SyntaxFactory.ClassDeclaration($"{applyToClass.Identifier}")
                                            .WithModifiers(classModifiers);

            if (applyToClass.TypeParameterList != null)
            {
                partialClass = partialClass.WithTypeParameterList(applyToClass.TypeParameterList);
            }

            if (applyToClass.ConstraintClauses != null)
            {
                partialClass = partialClass.WithConstraintClauses(applyToClass.ConstraintClauses);
            }

            var returnType = CodeGenUtil.TypeFromClass(applyToClass);

            var fields = applyToClass.Members.Where(m => m is FieldDeclarationSyntax)
                                             .Select(m => m as FieldDeclarationSyntax)
                                             .Where(m => m.Modifiers.Any(SyntaxKind.PublicKeyword))
                                             .Where(m => m.Modifiers.Any(SyntaxKind.ReadOnlyKeyword))
                                             .Where(m => !m.Modifiers.Any(SyntaxKind.StaticKeyword))
                                             .ToList();

            partialClass = CodeGenUtil.AddWith(context, partialClass, returnType, fields);
            partialClass = CodeGenUtil.AddLenses(partialClass, returnType, fields);

            return Task.FromResult<SyntaxList<MemberDeclarationSyntax>>(results.Add(partialClass));
        }
    }
}
