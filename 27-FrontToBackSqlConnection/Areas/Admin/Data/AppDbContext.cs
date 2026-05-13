using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Areas.Admin.Data
{
    public class AppDbContext:DbContext
    {
     
	    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
	}
}

