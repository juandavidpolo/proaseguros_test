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
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors("AllowAllOrigins")]
  public class FavoritesApiController : ControllerBase
  {
    private readonly AplicationDbContext _dbContext;

    public FavoritesApiController(AplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    // POST: api/Favorites
    [HttpPost]
    public async Task<IActionResult> CreateFavorite([FromBody] FavoritesModel favoritesModel)
    {
      try
      {
        Console.WriteLine($"Received payload: {favoritesModel.Name}");
        _dbContext.Favorites.Add(favoritesModel);
        await _dbContext.SaveChangesAsync();

        return Ok(new {
          success = true,
          message = "Lugar añadido a favoritos",
          fsqId = favoritesModel.Fsq_id
      });
      }
      catch (Exception ex)
      {
        var response = new
        {
          success = false,
          message = "An error occurred while processing the request.",
          error = ex.Message
        };
        return BadRequest(response);
      }
    }

    // PUT: api/Favorites/user_id/Fsq_id
    [HttpPut("{user_id}/{fsq_id}")]
    public async Task<IActionResult> SoftDeleteFavorite(int user_id, string fsq_id)
    {
      try
      {
        var favorite = await _dbContext.Favorites.FirstOrDefaultAsync(f => f.Fsq_id == fsq_id && f.User_id == user_id && f.Deleted_at == null);

        if (favorite == null)
        {
          return Ok(new
          {
            success = false,
            message = "Lugar no incluido en favoritos",
          });
        }

        favorite.Deleted_at = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return Ok(new {
          success = true,
          message = "Lugar eliminado de favoritos",
          fsqId = fsq_id
        });
      }
      catch (Exception ex)
      {
        var response = new
        {
          success = false,
          message = "An error occurred while processing the request.",
          error = ex.Message
        };
        return BadRequest(response);
      }
    }
  }
}
