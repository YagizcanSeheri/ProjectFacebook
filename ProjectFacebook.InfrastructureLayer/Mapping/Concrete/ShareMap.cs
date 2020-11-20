using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.InfrastructureLayer.Mapping.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.InfrastructureLayer.Mapping.Concrete
{
    public class ShareMap : BaseMap<Share>
    {
        public override void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.HasKey(x => new { x.PostId, x.AppUserId });
            base.Configure(builder);
        }
    }
}
