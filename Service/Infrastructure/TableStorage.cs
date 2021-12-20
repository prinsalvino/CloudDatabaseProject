using Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudDatabaseProject.Infrastructure
{
    public class TableStorage : ITableStorage
    {
        const string tableName = "OrderDescription";
        CloudTable table;

        public TableStorage()
        {
            CloudStorageAccount storageAccount = StorageAccountSettings.CreateStorageAccountFromConnectionString();

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            table = tableClient.GetTableReference(tableName);
        }
        public List<OrderDescription> GetAllOrders()
        {
            List<OrderDescription> lstOrd = new List<OrderDescription>();

            TableQuery<OrderDescription> query = new TableQuery<OrderDescription>()
                   .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "OrderDescription"));

            foreach (OrderDescription order in table.ExecuteQuerySegmentedAsync(query, null).Result)
            {
                lstOrd.Add(order);
            }
            return lstOrd;
        }

        public List<OrderDescription> GetOrderById(int id)
        {
            List<OrderDescription> lstOrd = new List<OrderDescription>();

            TableQuery<OrderDescription> query = new TableQuery<OrderDescription>()
                   .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString()));

            foreach (OrderDescription order in table.ExecuteQuerySegmentedAsync(query, null).Result)
            {
                lstOrd.Add(order);
            }
            return lstOrd;
        }

        public async Task CreateNewTable()
        {
            try
            {
                var result = await table.CreateIfNotExistsAsync();
            }
            catch (StorageException ex)
            {
            }
        }

        public async Task InsertRecordToTable(OrderDescription order)
        {

            try
            {
                TableOperation tableOperation = TableOperation.Insert(order);
                TableResult result = await table.ExecuteAsync(tableOperation);
                OrderDescription insertedCustomer = result.Result as OrderDescription;
                Console.WriteLine(insertedCustomer);
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public async Task UpdateRecordInTable(int orderId, string review)
        {
            string partitionKey  = "OrderDescription";
            string rowKey = orderId.ToString();

            OrderDescription orderEntity = await RetrieveRecord(partitionKey, rowKey);
            if (orderEntity is not null)
            {
                orderEntity.Review = review;
                TableOperation tableOperation = TableOperation.Replace(orderEntity);
                var result = await table.ExecuteAsync(tableOperation);
            }
        }
        public async Task DeleteRecordinTable(OrderDescription order)
        {
            string partitionKey = "OrderDescription";
            string rowKey = order.orderId.ToString();

            OrderDescription orderEntity = await RetrieveRecord(partitionKey, rowKey);
            if (orderEntity is not null)
            {
                TableOperation tableOperation = TableOperation.Delete(orderEntity);
                var result = await table.ExecuteAsync(tableOperation);
            }
        }
        private async Task<OrderDescription> RetrieveRecord(string partitionKey, string rowKey)
        {
            TableOperation tableOperation = TableOperation.Retrieve<OrderDescription>(partitionKey, rowKey);
            TableResult tableResult = await table.ExecuteAsync(tableOperation);
            return tableResult.Result as OrderDescription;
        }
    }
}
