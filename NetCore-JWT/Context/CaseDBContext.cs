using Microsoft.EntityFrameworkCore;
using NetCore_JWT.Models;

namespace NetCore_JWT.Context
{
	public class CaseDBContext : DbContext
	{
		public CaseDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Car> Cars { get; set; }
	}
}
