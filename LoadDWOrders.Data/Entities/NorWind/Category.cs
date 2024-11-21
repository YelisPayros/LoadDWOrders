namespace LoadDWOrders.Data.Entities.NorWind
{
    public class Category
    {
        public int CategoryID { get; set; }  // Clave primaria
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
    }
}
