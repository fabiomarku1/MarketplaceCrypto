using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models;

public class Watchlist
{
    [Key] public int Id { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }
    public DateTime DateCreated { get; set; }

    public virtual IEnumerable<CryptoList>? CryptoLists { get; set; }
}

