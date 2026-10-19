using System;
using System.Collections.Generic;

using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
    /// <summary>Basic generator that just returns a preconstructed attribute table.</summary>
    public class SimpleAttributeTableGenerator
        : CmsAttributeTableGenerator
    {
        private readonly AttributeTable attributes;

        public SimpleAttributeTableGenerator(
            AttributeTable attributes)
        {
            this.attributes = attributes;
        }

        /// <summary>Return the preconstructed attribute table.</summary>
        /// <param name="parameters">The generation parameters, which are ignored.</param>
        public virtual AttributeTable GetAttributes(IDictionary<CmsAttributeTableParameter, object> parameters)
        {
            return attributes;
        }
    }
}
