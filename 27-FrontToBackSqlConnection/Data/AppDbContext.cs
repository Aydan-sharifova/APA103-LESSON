using System;
using _27_FrontToBackSqlConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Data
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			DateTime seedDate = new(2026, 5, 8);

			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(18, 2);

			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "American Marigold", Description = "A bright flowering plant for balconies, gardens, and warm indoor corners.", Image = "1-1-270x300.jpg", HoverImage = "1-2-270x300.jpg", Price = 23.45m, IsFeatured = true, IsBestSeller = true, CreatedAt = seedDate },
				new Product { Id = 2, Name = "Black Eyed Susan", Description = "Compact, cheerful blooms with easy care needs and long seasonal color.", Image = "1-2-270x300.jpg", HoverImage = "1-3-270x300.jpg", Price = 25.45m, IsFeatured = true, CreatedAt = seedDate },
				new Product { Id = 3, Name = "Bleeding Heart", Description = "Elegant heart-shaped flowers for soft shade and decorative garden beds.", Image = "1-3-270x300.jpg", HoverImage = "1-4-270x300.jpg", Price = 30.45m, IsNew = true, CreatedAt = seedDate },
				new Product { Id = 4, Name = "Bloody Cranesbill", Description = "A resilient perennial with rich color and a tidy spreading habit.", Image = "1-4-270x300.jpg", HoverImage = "1-5-270x300.jpg", Price = 45.00m, IsBestSeller = true, CreatedAt = seedDate },
				new Product { Id = 5, Name = "Butterfly Weed", Description = "A sun-loving plant with vivid blooms and pollinator-friendly growth.", Image = "1-5-270x300.jpg", HoverImage = "1-6-270x300.jpg", Price = 50.45m, IsNew = true, CreatedAt = seedDate },
				new Product { Id = 6, Name = "Common Yarrow", Description = "Hardy clusters of flowers that fit naturally into low-maintenance gardens.", Image = "1-6-270x300.jpg", HoverImage = "1-7-270x300.jpg", Price = 65.00m, IsFeatured = true, CreatedAt = seedDate }
			);
		}
	}
}
