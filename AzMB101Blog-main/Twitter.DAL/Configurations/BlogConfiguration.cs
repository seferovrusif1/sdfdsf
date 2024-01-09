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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(t => t.UserId)
                .IsRequired();   
            builder.Property(t => t.UpdateCount)
                .IsRequired();
        }

      
    }
}
