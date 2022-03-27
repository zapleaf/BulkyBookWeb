using Microsoft.EntityFrameworkCore;

using BulkyBookWeb.Models;

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// 1:18 - options will be sent to constructor and the options will be passed to the base class
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }


    }

}
