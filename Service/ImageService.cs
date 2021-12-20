using CloudDatabaseProject.Infrastructure;
using DAL.RepoInterfaces;
using Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Service.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ImageService : IImageService
    {
        IImageRepository _repository;

        private const string CONTAINERPREFIX = "clouddbstorage";


        public ImageService(IImageRepository repository)
        {
            _repository = repository;
        }

        public async Task AddImage(Image image, string ImageToUpload)
        {
            CloudStorageAccount storageAccount = StorageAccountSettings.CreateStorageAccountFromConnectionString();

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(CONTAINERPREFIX);
            try
            {
                // Retry policy - optional
                BlobRequestOptions optionsWithRetryPolicy = new BlobRequestOptions() { RetryPolicy = new Microsoft.WindowsAzure.Storage.RetryPolicies.LinearRetry(TimeSpan.FromSeconds(20), 4) };

                await container.CreateIfNotExistsAsync(optionsWithRetryPolicy, null);
            }
            catch (StorageException ex)
            {
                //logger.LogError(ex.Message);
            }

            // await container.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            // Upload a BlockBlob to the newly created container
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(ImageToUpload);

            // Browser now knows it as an image.
            var extension = Path.GetExtension(ImageToUpload);

            Console.WriteLine(extension);
            if (extension == ".jpg")
            {
                blockBlob.Properties.ContentType = "image/jpg";
            }
            else if (extension == ".png")
            {
                blockBlob.Properties.ContentType = "image/png";
            }
            else if (extension == ".jpeg")
            {
                blockBlob.Properties.ContentType = "image/jpeg";
            }
            else
            {
                throw new StorageException();
            }


            try
            {
                await blockBlob.UploadFromFileAsync(ImageToUpload);
                image.Link = blockBlob.Uri.AbsoluteUri;
            }
            catch (StorageException ex)
            {
                //logger.LogError(ex.Message);
            }
            _repository.Add(image);
        }

        public void DeleteImage(int id)
        {
            Image image = _repository.GetSingle(id);
            _repository.Delete(image);
        }

        public Image GetImage(int id)
        {
            return _repository.GetSingle(id);
        }

        public IEnumerable<Image> GetImages()
        {
            return _repository.GetAll();
        }

        public void UpdateImage(Image image)
        {
            _repository.Update(image);
        }
    }
}
