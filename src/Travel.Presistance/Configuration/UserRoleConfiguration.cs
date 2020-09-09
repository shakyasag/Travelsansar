using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Entities;
using Travel.Entities.Entities;

namespace Travel.Presistance.Configuration
{
   public  class UserRoleConfiguration:IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //builder.Property(x => x.Title).HasMaxLength(100);
            //builder.Property(x => x.ImageUrl).HasMaxLength(500).IsRequired();

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.HasOne(u => u.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserId)
                 .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne(r => r.Role)
                .WithMany(r => r.Userrole)
                .HasForeignKey(r => r.RoleId)
                 .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

          

        }

       
    }
}
