using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.InfrastructureLayer.Mapping.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.InfrastructureLayer.Mapping.Concrete
{
    public class FollowMap : BaseMap<Follow>
    {
        public override void Configure(EntityTypeBuilder<Follow> builder)
        {
            
            builder.HasKey(x => new { x.FollowerId, x.FollowingId });

            base.Configure(builder);
        }
    }
}
