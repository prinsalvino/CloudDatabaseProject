using Domain;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDatabaseProject.Infrastructure
{
    public interface ITableStorage
    {
        public List<OrderDescription> GetAllOrders();
        public List<OrderDescription> GetOrderById(int id);

        public Task CreateNewTable();

        public Task InsertRecordToTable(OrderDescription order);

        public Task UpdateRecordInTable(int orderId, string review);

        public Task DeleteRecordinTable(OrderDescription order);

    }
}
