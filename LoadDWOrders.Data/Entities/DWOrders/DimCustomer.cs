
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("DimCustomer")]
    public class DimCustomer
    {
        [Key]
        public int CustomerKey { get; set; }
        public string CustomerID { get; set; }
        public string? CompanyName { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }

    }
}
