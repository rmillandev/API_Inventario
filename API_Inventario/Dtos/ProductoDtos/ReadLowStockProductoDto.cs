using System.Text.Json.Serialization;

namespace API_Inventario.Dtos.ProductoDtos
{
    public class ReadLowStockProductoDto : ReadProductoDTO
    {
        [JsonPropertyOrder(100)]
        public string StockBajo { get; set; }
    }
}
