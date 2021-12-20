using CloudDatabaseProject.Infrastructure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderQueue
{
    public class QueueStorage : IQueueStorage
    {
        private readonly string queueName;
        private CloudQueueClient queueClient;
        private CloudQueue queue;

        public QueueStorage()
        {
            //this.connectionString = configuration["ConnectionStrings:DefaultStorageConnection"];
            CloudStorageAccount storageAccount = StorageAccountSettings.CreateStorageAccountFromConnectionString();
            this.queueName = "orderqueue";

            queueClient = storageAccount.CreateCloudQueueClient();
            queue = queueClient.GetQueueReference(queueName);
        }

        public async Task CreateMessage(string message)
        {
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            try
            {
                await queue.AddMessageAsync(new CloudQueueMessage(message));
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public async Task DeleteMessage()
        {
            CloudQueueMessage message = await queue.GetMessageAsync();
            if (message != null)
            {

                try
                {
                    await queue.DeleteMessageAsync(message);
                }
                catch (StorageException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }

        public async Task<string> PeekMessage()
        {
            try
            {
                CloudQueueMessage peekedMessage = await queue.PeekMessageAsync();
                if (peekedMessage != null)

                    return peekedMessage.AsString;
            }
            catch (StorageException ex)
            {
                Console.WriteLine(ex.Message.ToString());

            }
            return "message not peekable";
        }
    }
}
