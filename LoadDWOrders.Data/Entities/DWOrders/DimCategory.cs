
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("DimCategory")]
    public class DimCategory
    {
        [Key]
        public int CategoryKey { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
