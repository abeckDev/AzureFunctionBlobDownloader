using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace AbeckDev.AzureFunctionBlobDownloader.Services
{
	public static class BlobStorageService
	{

        public static async Task<MemoryStream> GetBlob(string blobName)
        {

            string ConnectionString = System.Environment.GetEnvironmentVariable("StorageAccountConnectionString");
            string Container = System.Environment.GetEnvironmentVariable("blobContainer");
            string debugBlobName = blobName;

            BlobClient blobClient = new BlobClient(ConnectionString,Container, blobName);

            var stream = new MemoryStream();
            await blobClient.DownloadToAsync(stream);
            stream.Position = 0;

            return stream;
            
        }
    }
}

