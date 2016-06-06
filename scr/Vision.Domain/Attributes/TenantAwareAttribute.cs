using System;
using System.Linq;
using System.Data.Metadata.Edm;

namespace Vision.Domain.Attributes
{
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class TenantAwareAttribute : Attribute
        {
            public const string TenantAnnotation = "TenantAnnotation";
            public const string TenantIdFilterParameterName = "TenantIdParameter";

            public string ColumnName { get; private set; }

            public TenantAwareAttribute(string columnName)
            {
                ColumnName = columnName;
            }

            public static string GetTenantColumnName(EdmType type)
            {
                MetadataProperty annotation = type.MetadataProperties.SingleOrDefault(
                    p => p.Name.EndsWith(string.Format("customannotation:{0}", TenantAnnotation)));

                return annotation == null ? null : (string)annotation.Value;
            }
        
    }
}
