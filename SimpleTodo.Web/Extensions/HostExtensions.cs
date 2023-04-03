namespace SimpleTodo.Web.Extensions;

using SimpleTodo.Infrastructure;

public static class HostExtensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TodoDbContext>();
                if (context.Database.EnsureCreated())
                {
                    DbInitializer.Initialize(context);
                }
            }
        }
    }
}