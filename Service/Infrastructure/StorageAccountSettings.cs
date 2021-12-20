

using Microsoft.WindowsAzure.Storage;

namespace CloudDatabaseProject.Infrastructure
{
    public class StorageAccountSettings
    {
        public static CloudStorageAccount CreateStorageAccountFromConnectionString()
        {
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=clouddbprojectstorage;AccountKey=QSD5Xp/VSBecWKJEzLbI2fcowjJqlvzEXuYKiUQH6NUmnAlwh3cPBE+p0pIpWRQr843LnEMB5wCtY3o0XMoBdQ==;EndpointSuffix=core.windows.net");

            return storageAccount;
        }
    }
}
