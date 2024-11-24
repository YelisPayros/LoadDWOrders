using LoadDWOrders.Data.Context;
using LoadDWOrders.Data.Entities.DWOrders;
using LoadDWOrders.Data.Interfaces;
using LoadDWOrders.Data.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoadDWOrders.Data.Services
{
    public class DataServiceDWOrders : IDataServiceDWOrders
    {
        private readonly NorwindContext _norwindContext;
        private readonly DWOrdersContext _dWOrdersContext;
        private readonly ILogger<DataServiceDWOrders> _logger;

        public DataServiceDWOrders(NorwindContext norwindContext, DWOrdersContext dWOrdersContext, ILogger<DataServiceDWOrders> logger)
        {
            _norwindContext = norwindContext;
            _dWOrdersContext = dWOrdersContext;
            _logger = logger;
        }

        private async Task ClearDimTables()
        {
            // Clear fact tables first to avoid foreign key constraint issues
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM FactSales");
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM FactCustomersAttended");

            // Clear dimension tables
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM DimCustomer");
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM DimEmployee");
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM DimShipper");
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM DimCategory");
            await _dWOrdersContext.Database.ExecuteSqlRawAsync("DELETE FROM DimProduct");
        }

        public async Task<OperationResult> LoadDw()
        {
            OperationResult result = new OperationResult();
            try
            {
                await ClearDimTables();   

                await LoadDimEmployee();
                await LoadDimShippers();
                await LoadDimCategory();
                await LoadDimCustomers();
                await LoadDimProduct();
                await LoadFactSales();
                await LoadFactCustomersAttended();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando el DWH Orders: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        private async Task<OperationResult> LoadDimCustomers()
        {
            OperationResult result = new OperationResult();
            try
            {
                var customers = await _norwindContext.Customers.Select(c => new DimCustomer
                {
                    CustomerID = c.CustomerID,
                    CompanyName = c.CompanyName,
                    Country = c.Country,
                    Region = c.Region,
                    City = c.City
                }).ToListAsync();

                _logger.LogInformation("Retrieved {count} customers from NorwindContext", customers.Count);

                await _dWOrdersContext.DimCustomers.AddRangeAsync(customers);
                await _dWOrdersContext.SaveChangesAsync();

                _logger.LogInformation("Inserted {count} customers into DWOrdersContext", customers.Count);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimensión clientes: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        private async Task<OperationResult> LoadDimEmployee()
        {
            OperationResult result = new OperationResult();
            try
            {
                var employees = await _norwindContext.Employees
                    .Select(emp => new DimEmployee
                    {
                        EmployeeID = emp.EmployeeID,
                        EmployeeName = string.Concat(emp.FirstName, " ", emp.LastName),
                        Title = emp.Title,
                        City = emp.City,
                        Country = emp.Country
                    }).ToListAsync();

                _logger.LogInformation("Retrieved {count} employees from NorwindContext", employees.Count);

                await _dWOrdersContext.DimEmployees.AddRangeAsync(employees);
                await _dWOrdersContext.SaveChangesAsync();

                _logger.LogInformation("Inserted {count} employees into DWOrdersContext", employees.Count);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimensión empleados: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        private async Task<OperationResult> LoadDimShippers()
        {
            OperationResult result = new OperationResult();
            try
            {
                var shippers = await _norwindContext.Shippers.Select(s => new DimShipper
                {
                    ShipperID = s.ShipperID,
                    ShipperName = s.CompanyName
                }).ToListAsync();

                _logger.LogInformation("Retrieved {count} shippers from NorwindContext", shippers.Count);

                await _dWOrdersContext.DimShippers.AddRangeAsync(shippers);
                await _dWOrdersContext.SaveChangesAsync();

                _logger.LogInformation("Inserted {count} shippers into DWOrdersContext", shippers.Count);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimensión shippers: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        private async Task<OperationResult> LoadDimCategory()
        {
            OperationResult result = new OperationResult();
            try
            {
                var categories = await _norwindContext.Categories.Select(c => new DimCategory
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                }).ToListAsync();

                _logger.LogInformation("Retrieved {count} categories from NorwindContext", categories.Count);

                await _dWOrdersContext.DimCategories.AddRangeAsync(categories);
                await _dWOrdersContext.SaveChangesAsync();

                _logger.LogInformation("Inserted {count} categories into DWOrdersContext", categories.Count);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimensión categorías: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        private async Task<OperationResult> LoadDimProduct()
        {
            OperationResult result = new OperationResult();
            try
            {
                var products = await _norwindContext.Products.Select(p => new DimProduct
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    CategoryName = _norwindContext.Categories.FirstOrDefault(c => c.CategoryID == p.CategoryID).CategoryName,
                    UnitPrice = p.UnitPrice,
                    QuantityPerUnit = p.QuantityPerUnit
                }).ToListAsync();

                _logger.LogInformation("Retrieved {count} products from NorwindContext", products.Count);

                await _dWOrdersContext.DimProducts.AddRangeAsync(products);
                await _dWOrdersContext.SaveChangesAsync();

                _logger.LogInformation("Inserted {count} products into DWOrdersContext", products.Count);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la dimensión productos: {ex.Message}";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }


        private async Task<OperationResult> LoadFactSales() 
        {
            OperationResult result = new OperationResult();
            try
            {
                var ventas= await _norwindContext.VwFactSales.AsNoTracking().ToListAsync();

                //ventas.ForEach(async cd =>
                //{
                   
                //});

                foreach(var venta in ventas)
                {
                    var customer = await _dWOrdersContext.DimCustomers.SingleOrDefaultAsync(cust => cust.CustomerID == venta.CustomerId);
                    var employee = await _dWOrdersContext.DimEmployees.SingleOrDefaultAsync(emp => emp.EmployeeID == venta.EmployeeId);
                    var shippers = await _dWOrdersContext.DimShippers.SingleOrDefaultAsync(ship => ship.ShipperID == venta.ShipperId);
                    var product = await _dWOrdersContext.DimProducts.SingleOrDefaultAsync(prod => prod.ProductID == venta.ProductId);


                    FactSales factSales = new FactSales()
                    {
                        CantidadVentas = venta.Cantidad,
                        City = venta.City,
                        CustomerKey = customer.CustomerKey,
                        EmployeeKey = employee.EmployeeKey,
                        ProductKey = product.ProductKey,
                        ShipperKey = shippers.ShipperKey,
                        TotalVentas = venta.TotalVentas,
                        EmployeeName= employee.EmployeeName,
                        ProductName = product.ProductName,

                        
                    };
                    await _dWOrdersContext.FactSales.AddAsync(factSales);

                    await _dWOrdersContext.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la FactSales: {ex.Message}";
            
            }
            return result;
        }

        private async Task<OperationResult> LoadFactCustomersAttended()
        {
            OperationResult result = new OperationResult();
            try
            {
                var clientes = await _norwindContext.VwFactCustomersAtendeds.AsNoTracking().ToListAsync();
                foreach (var cliente in clientes)
                {
                    var employee = await _dWOrdersContext.DimEmployees.SingleOrDefaultAsync(emp => emp.EmployeeID == cliente.EmployeeId);
                   




                    FactCustomersAtended factCustomersAtended = new FactCustomersAtended()
                    {

                        EmployeeKey= employee.EmployeeKey,
                        TotalCustomersServed= cliente.TotalCustomerServed
                        

                    };
                    await _dWOrdersContext.FactCustomersAtendeds.AddAsync(factCustomersAtended);

                    await _dWOrdersContext.SaveChangesAsync();
                }
             }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = $"Error cargando la Fact clientes atendidos: {ex.Message}";

            }
            return result;
        }
    }
}

