
using LoadDWOrders.Data.Entities.DWOrders;
using LoadDWOrders.Data.Results;

namespace LoadDWOrders.Data.Interfaces
{
    public interface IDataServiceDWOrders
    {

        Task<OperationResult> LoadDw();

    }
}
