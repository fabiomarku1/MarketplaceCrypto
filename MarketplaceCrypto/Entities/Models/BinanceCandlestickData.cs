using System.Text.Json;
using System.Text.Json.Serialization;

namespace Entities.Models;

public class BinanceCandlestickData
{
    public string Symbol { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public DateTime Time { get; set; }
}
