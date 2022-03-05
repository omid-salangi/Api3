using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utilities;
using Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext  : IdentityDbContext<User,Roles,string>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryToPost> CategoryToPosts { get; set; }
        public DbSet<Comments> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryToPost>().HasKey(cp => new { cp.CategoryId, cp.PostId });
            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();

        }
    }
}
