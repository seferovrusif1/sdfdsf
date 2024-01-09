﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using Twitter.Core.Entities;
using Twitter.Core.Entities.Common;
using Twitter.DAL.Configurations;

namespace Twitter.DAL.Contexts
{
    public class TwitterContext : IdentityDbContext
    {
        public TwitterContext(DbContextOptions opt) : base(opt)
        {
        }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedTime = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        //public override Task<int> UpdateChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker.Entries<Blog>();
        //    foreach (var entry in entries)
        //    {
        //        if (entry.State == EntityState.Modified)
        //            entry.Entity.LastUpdateTime = DateTime.UtcNow;
        //    }
        //    return base.SaveChangesAsync(cancellationToken);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(TopicConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
