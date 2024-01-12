using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Core.Entities;

namespace Twitter.DAL.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(t => t.AppUserId)
                .IsRequired();
            builder.Property(t => t.ParentCommentId)
              .IsRequired();
            builder.Property(t => t.BlogId)
                .IsRequired();
            builder.HasOne(x => x.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(u => u.AppUserId);
            
            builder.HasOne(x => x.Blog)
                .WithMany(u => u.Comments)
                .HasForeignKey(u => u.BlogId)
                .OnDelete(DeleteBehavior.NoAction);

          
            builder.HasOne(x => x.ParentComment)
                            .WithMany(u => u.ChildComments)
                            .HasForeignKey(u => u.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
