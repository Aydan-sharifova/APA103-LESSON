using System;
using _27_FrontToBackSqlConnection.Models;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Data
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

		public DbSet<Slider> Sliders { get; set; }
	}
}

