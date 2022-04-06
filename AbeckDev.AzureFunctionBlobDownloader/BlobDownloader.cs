using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AbeckDev.AzureFunctionBlobDownloader.Services;
using System.Net.Http;

namespace AbeckDev.AzureFunctionBlobDownloader
{
    public static class BlobDownloader
    {
        [FunctionName("BlobDownloader")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (!req.Query.ContainsKey("blobId"))
            {
                return new BadRequestObjectResult("Please provide the blobId query parameter!");
            }

            string blobId = req.Query["blobId"];
            var responseStream = await BlobStorageService.GetBlob(blobId);

            return new FileContentResult(responseStream.ToArray(), "application/octet-stream")
            {
                FileDownloadName = blobId,
            }; 
        }
    }
}

