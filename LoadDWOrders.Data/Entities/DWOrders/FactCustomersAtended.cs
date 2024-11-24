using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("FactCustomersAttended", Schema = "dbo")]
    public class FactCustomersAtended
    {
        [Key]
        public int AttendedKey { get; set; }

        public int? EmployeeKey { get; set; }


        public int? TotalCustomersServed { get; set; }
    }
}
