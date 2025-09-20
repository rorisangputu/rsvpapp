using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using rsvp.data.Models;

namespace rsvp.data.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        { }

        public DbSet<Event> Events { get; set; }
        public DbSet<Rsvp> Rsvps { get; set; }
        public DbSet<EventCategory> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>(entity =>
            {

                // Relationship: Event -> User (CreatedBy)
                entity.HasOne(e => e.CreatedByUser)
                    .WithMany(u => u.EventsCreated)
                    .HasForeignKey(e => e.CreatedByUserId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of user if they have events

                // Relationship: Event -> EventCategory
                entity.HasOne(e => e.EventCategory)
                    .WithMany(c => c.Events)
                    .HasForeignKey(e => e.EventCategoryId)
                    .OnDelete(DeleteBehavior.SetNull); // Set to null if category is deleted

            });

            builder.Entity<Rsvp>(entity =>
            {

                // Relationship: Rsvp -> User
                entity.HasOne(r => r.User)
                    .WithMany(u => u.RSVPs)
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete RSVPs if user is deleted

                // Relationship: Rsvp -> Event
                entity.HasOne(r => r.Event)
                    .WithMany(e => e.RSVPs)
                    .HasForeignKey(r => r.EventId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete RSVPs if event is deleted

                // Composite unique constraint: One RSVP per user per event
                entity.HasIndex(r => new { r.UserId, r.EventId })
                    .IsUnique();
            });

            // Optional: Add some indexes for performance
            builder.Entity<Event>()
                .HasIndex(e => e.Date);

            builder.Entity<Event>()
                .HasIndex(e => e.CreatedByUserId);

            builder.Entity<Event>()
                .HasIndex(e => e.EventCategoryId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}