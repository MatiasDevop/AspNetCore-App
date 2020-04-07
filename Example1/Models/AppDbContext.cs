﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example1.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Friend> Friends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().HasData(new Friend 
            {   Id = 1, 
                Name = "PEPE",
                City = Province.Alava,
                Email = "mail@mail.com"
            },
            new Friend
            {
                Id = 2,
                 Name = "Juan",
                City = Province.Alicante,
                Email = "pejjp@mail.com"
            },
             new Friend
             {
                 Id = 3,
                 Name = "Laura",
                 City = Province.Caceres,
                 Email = "lllmail@mail3.com"
             }

            );
        }
    }
}
