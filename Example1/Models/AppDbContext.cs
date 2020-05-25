using Example1.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class AppDbContext: IdentityDbContext<UserApplication> //DbContext inheritance form IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            //modelBuilder.Entity<Friend>().HasData(new Friend 
            //{   Id = 1, 
            //    Name = "PEPE",
            //    City = Province.Alava,
            //    Email = "mail@mail.com"
            //},
            //new Friend
            //{
            //    Id = 2,
            //     Name = "Juan",
            //    City = Province.Alicante,
            //    Email = "pejjp@mail.com"
            //},
            // new Friend
            // {
            //     Id = 3,
            //     Name = "Laura",
            //     City = Province.Caceres,
            //     Email = "lllmail@mail3.com"
            // }

            //);
        }
    }
}
