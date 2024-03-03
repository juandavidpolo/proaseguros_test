using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using proaseguros_test.Data;
using proaseguros_test.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace proaseguros_test.Controllers
{
  public class FavoritesController : Controller
  {
    private readonly AplicationDbContext _dbContext;

    public FavoritesController(AplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IActionResult> Index()
    {
      var userId = 1;

      // Fetch favorites for the user from the database
      var favorites = await _dbContext.Favorites
          .Where(f => f.User_id == userId && f.Deleted_at == null)
          .ToListAsync();

      // Pass the fetched favorites directly to the view
      return View("Favorites", favorites);
    }

  }
}
