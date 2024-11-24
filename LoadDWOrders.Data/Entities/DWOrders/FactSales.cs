
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("FactSales", Schema = "dbo")]
    public class FactSales
    {
        
        [Key]
        public int SaleKey { get; set; }

        //public int? TimeKey { get; set; }

        public int? CustomerKey { get; set; }

        public int? EmployeeKey { get; set; }

        public int? ProductKey { get; set; }

        public int? ShipperKey { get; set; }

        //public int? CategoryKey { get; set; }

        public int? CantidadVentas { get; set; }

        public double? TotalVentas { get; set; }

        public string ProductName { get; set; }

        public string EmployeeName { get; set; }

        public string City { get; set; }
    }
}
