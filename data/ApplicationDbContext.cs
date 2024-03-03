using Microsoft.EntityFrameworkCore;
using proaseguros_test.Models;

namespace proaseguros_test.Data
{
  public class AplicationDbContext : DbContext
  {
    public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
    {
      Favorites = Set<FavoritesModel>();
    }
    public DbSet<FavoritesModel> Favorites { get; set; }
  }
}
