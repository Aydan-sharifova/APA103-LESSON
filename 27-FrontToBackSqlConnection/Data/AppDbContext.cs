using System;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Data
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
	}
}

