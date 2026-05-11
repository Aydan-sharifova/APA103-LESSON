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
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			DateTime seedDate = new(2026, 5, 8, 0, 0, 0, DateTimeKind.Utc);

			modelBuilder.Entity<Product>()
				.Property(p => p.Price)
				.HasPrecision(18, 2);

			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "House Plants", CreatedAt = seedDate },
				new Category { Id = 2, Name = "Garden Plants", CreatedAt = seedDate },
				new Category { Id = 3, Name = "Gift Plants", CreatedAt = seedDate }
			);

			modelBuilder.Entity<Slider>().HasData(
				new Slider
				{
					Id = 1,
					Title = "Plant For Healthy",
					Subtitle = "Discover Now",
					Desc = "Pronia, With 100% Natural, Organic & Plant Shop.",
					Image = "1-1-524x617.png",
					Order = 1,
					CreatedAt = seedDate
				},
				new Slider
				{
					Id = 2,
					Title = "Fresh Your Mind",
					Subtitle = "Discover Now",
					Desc = "Pronia, With 100% Natural, Organic & Plant Shop.",
					Image = "1-2-524x617.png",
					Order = 2,
					CreatedAt = seedDate
				}
			);

			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "American Marigold", Description = "A bright flowering plant for balconies, gardens, and warm indoor corners.", SKU = "PL-001", Image = "1-1-270x300.jpg", HoverImage = "1-2-270x300.jpg", Price = 23.45m, CategoryId = 2, IsFeatured = true, IsBestSeller = true, CreatedAt = seedDate },
				new Product { Id = 2, Name = "Black Eyed Susan", Description = "Compact, cheerful blooms with easy care needs and long seasonal color.", SKU = "PL-002", Image = "1-2-270x300.jpg", HoverImage = "1-3-270x300.jpg", Price = 25.45m, CategoryId = 2, IsFeatured = true, CreatedAt = seedDate },
				new Product { Id = 3, Name = "Bleeding Heart", Description = "Elegant heart-shaped flowers for soft shade and decorative garden beds.", SKU = "PL-003", Image = "1-3-270x300.jpg", HoverImage = "1-4-270x300.jpg", Price = 30.45m, CategoryId = 3, IsNew = true, CreatedAt = seedDate },
				new Product { Id = 4, Name = "Bloody Cranesbill", Description = "A resilient perennial with rich color and a tidy spreading habit.", SKU = "PL-004", Image = "1-4-270x300.jpg", HoverImage = "1-5-270x300.jpg", Price = 45.00m, CategoryId = 1, IsBestSeller = true, CreatedAt = seedDate },
				new Product { Id = 5, Name = "Butterfly Weed", Description = "A sun-loving plant with vivid blooms and pollinator-friendly growth.", SKU = "PL-005", Image = "1-5-270x300.jpg", HoverImage = "1-6-270x300.jpg", Price = 50.45m, CategoryId = 1, IsNew = true, CreatedAt = seedDate },
				new Product { Id = 6, Name = "Common Yarrow", Description = "Hardy clusters of flowers that fit naturally into low-maintenance gardens.", SKU = "PL-006", Image = "1-6-270x300.jpg", HoverImage = "1-7-270x300.jpg", Price = 65.00m, CategoryId = 3, IsFeatured = true, CreatedAt = seedDate }
			);

			modelBuilder.Entity<ProductImage>().HasData(
				new ProductImage { Id = 1, Image = "1-1-270x300.jpg", IsPrimary = true, ProductId = 1, CreatedAt = seedDate },
				new ProductImage { Id = 2, Image = "1-2-270x300.jpg", IsPrimary = false, ProductId = 1, CreatedAt = seedDate },
				new ProductImage { Id = 3, Image = "1-2-270x300.jpg", IsPrimary = true, ProductId = 2, CreatedAt = seedDate },
				new ProductImage { Id = 4, Image = "1-3-270x300.jpg", IsPrimary = false, ProductId = 2, CreatedAt = seedDate },
				new ProductImage { Id = 5, Image = "1-3-270x300.jpg", IsPrimary = true, ProductId = 3, CreatedAt = seedDate },
				new ProductImage { Id = 6, Image = "1-4-270x300.jpg", IsPrimary = true, ProductId = 4, CreatedAt = seedDate },
				new ProductImage { Id = 7, Image = "1-5-270x300.jpg", IsPrimary = true, ProductId = 5, CreatedAt = seedDate },
				new ProductImage { Id = 8, Image = "1-6-270x300.jpg", IsPrimary = true, ProductId = 6, CreatedAt = seedDate }
			);
		}
	}
}
