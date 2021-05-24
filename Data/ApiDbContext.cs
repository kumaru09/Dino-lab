using Dinolab.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dinolab
{
    public class ApiDbContext : IdentityDbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {

        }

        public DbSet<ItemList> ItemList {get; set;}
        public DbSet<BookingList> BookingList { get; set; }
        public DbSet<LabList> LabList { get; set; }
    }
}