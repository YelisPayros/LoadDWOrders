
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoadDWOrders.Data.Entities.DWOrders
{
    [Table("DimShipper")]
    public class DimShipper
    {
        [Key]
        public int ShipperKey { get; set; }
        public int ShipperID { get; set; }  
        public string ShipperName { get; set; }

       
       
    }
}
