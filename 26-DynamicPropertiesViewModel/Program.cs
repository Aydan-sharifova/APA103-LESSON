namespace _26_DynamicPropertiesViewModel;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseStaticFiles();

        app.MapControllerRoute(
            name: "Corporate",
            pattern: "korporativ-satislar",
            defaults: new { controller = "home", action = "corporativesales" }
        );

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=home}/{action=index}/{id?}"
        );

        app.Run();
    }
}
