
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("DimEmployee")]
    public class DimEmployee
    {
        [Key]
        public int EmployeeKey { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string? Title { get; set; }
        public string? City { get; set; }

        public string? Country { get; set; }

    }
}
