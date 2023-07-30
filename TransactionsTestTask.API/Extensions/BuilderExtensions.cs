using Microsoft.EntityFrameworkCore;
using TransactionsTestTask.DAL.Data;

namespace TransactionsTestTask.API.Extensions
{
    public static class BuilderExtensions
    {
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
