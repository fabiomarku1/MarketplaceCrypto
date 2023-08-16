using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class CryptoCurrency
{
    [Key] public int Id { get; set; }
    public string Symbol { get; set; }
    public DateTime DateCreated { get; set; }
}
