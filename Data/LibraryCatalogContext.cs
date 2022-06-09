using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class LibraryCatalogContext:IdentityDbContext
    {
        public LibraryCatalogContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } 
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Topic> Topics { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-AM9SN2K\\SQLEXPRESS; Database=LibraryContext; Trusted_connection=True");
        }
    }
}
