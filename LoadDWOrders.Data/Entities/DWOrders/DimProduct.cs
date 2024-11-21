
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("DimProduct")]
    public class DimProduct
    {
        [Key]
        public int ProductKey { get; set; }

        public string? CategoryName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }

        public string? QuantityPerUnit { get; set; }
    }
}
