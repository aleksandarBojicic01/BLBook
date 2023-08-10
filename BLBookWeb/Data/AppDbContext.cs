using Microsoft.EntityFrameworkCore;

namespace BLBookWeb.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			
		}
	}
}
