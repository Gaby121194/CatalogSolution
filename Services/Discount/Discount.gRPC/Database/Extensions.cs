using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Database
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            dbContext.Database.MigrateAsync();
            // We can add any database migration or initial setup code here if needed in the future
            return app;
        }
    }
}
