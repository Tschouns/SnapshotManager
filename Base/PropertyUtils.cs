//-----------------------------------------------------------------------
// <copyright file="BlowMeInTheShoes.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Base
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Provides methods to work with properties.
    /// </summary>
    public static class PropertyUtils
    {
        public static string GetPropertyName<TObject, TProperty>(Expression<Func<TObject, TProperty>> propertyExpression)
        {
            ArgumentChecks.AssertNotNull(propertyExpression, nameof(propertyExpression));

            var expressionBodyString = propertyExpression.Body.ToString();
            if (!expressionBodyString.Contains("."))
            {
                throw new ArgumentException("The expression does not contain any dots ('.').");
            }

            var elements = expressionBodyString.Split('.');
            if (elements.Length < 2)
            {
                throw new ArgumentException("The expression body is expected to contain at least two elements.");
            }

            // Pick the term after the first dot ('.').
            var lReferencedElement = elements[1];

            // Beautify.
            lReferencedElement = lReferencedElement.TrimEnd('(', ')');

            return lReferencedElement;
        }
    }
}
