using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.InfrastructureLayer.Mapping.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.InfrastructureLayer.Mapping.Concrete
{
    public class PostMap : BaseMap<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.HasMany(x => x.Comments)
              .WithOne(x => x.Post)
              .HasForeignKey(x => x.PostId);


            builder.HasMany(x => x.Likes)
              .WithOne(x => x.Post)
              .HasForeignKey(x => x.PostId);

            builder.HasMany(x => x.Shares)
              .WithOne(x => x.Post)
              .HasForeignKey(x => x.PostId);


            base.Configure(builder);
        }
    }
}
