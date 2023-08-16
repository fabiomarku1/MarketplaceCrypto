using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;
public class ApplicationUser:IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    [ForeignKey("WatchlistId")] public int? WatchlistId { get; set; }
    public Watchlist? Watchlist { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string? TokenHash { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }

}
