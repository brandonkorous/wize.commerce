using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wize.commerce.data.v1.Models;

namespace wize.commerce.odata.ModelConfigurations
{
    public class ShoppingCartItemOptionModelConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion version, string routePrefix)
        {
            switch (version.MajorVersion)
            {
                case 1:
                    BuildV1(builder);
                    break;
                default:
                    BuildDefault(builder);
                    break;
            }
        }

        private EntityTypeConfiguration<ShoppingCartItemOption> BuildDefault(ODataModelBuilder builder)
        {
            var model = builder.EntitySet<ShoppingCartItemOption>("ShoppingCartItemOptions").EntityType;
            model.HasKey(m => new { m.ShoppingCartItemId, m.ProductTraitId, m.ProductTraitOptionId });
            return model;
        }

        private void BuildV1(ODataModelBuilder builder)
        {
            BuildDefault(builder);
        }
    }
}
