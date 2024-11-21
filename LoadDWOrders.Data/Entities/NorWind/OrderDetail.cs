

namespace LoadDWOrders.Data.Entities.NorWind
{
    public class OrderDetail
    {
        public int OrderID { get; set; }  // Clave primaria
        public int? ProductID { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public float? Discount { get; set; }
    }
}
