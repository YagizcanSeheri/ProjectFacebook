using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectFacebook.DomainLayer.Entities.Concrete;
using ProjectFacebook.InfrastructureLayer.Mapping.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.InfrastructureLayer.Mapping.Concrete
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            #region CoreIdentityMap
            //Identity sınıfından gelen özellikleride mapping edebiliriz
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName);
            builder.Property(x => x.NormalizedUserName);
            #endregion
            builder.Property(x => x.Name).IsRequired(false);
            builder.Property(x => x.ImagePath).IsRequired(false);


            builder.HasMany(x => x.Posts)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Likes)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Shares)
                .WithOne(x => x.AppUser)
                .HasForeignKey(x => x.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Followers)
                .WithOne(x => x.Follower)
                .HasForeignKey(x => x.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Following)
                .WithOne(x => x.Following)
                .HasForeignKey(x => x.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }
    }
}
