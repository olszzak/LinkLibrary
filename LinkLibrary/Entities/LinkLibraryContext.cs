using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Entities
{
    public class LinkLibraryContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public LinkLibraryContext(DbContextOptions<LinkLibraryContext> options) : base(options)
        {
            //Database.Migrate();
        }
        public DbSet<Link> Links { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
