using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
namespace AzuriteDemo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var a = DateTimeOffset.Now;
            Console.WriteLine($"the time is *****************{a}");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                
                await Task.Delay(1000, stoppingToken);

                CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=picturesblobstorage;AccountKey=yCk4Z5pshM/1FTn/PRoMLTveIa4NAX2EKju8pmsZccZALtcFZTGoQlrfZFoJureoAU0aXm1VP9Ah+AStO+23gw==;EndpointSuffix=core.windows.net;");

                // Create a blob client for interacting with the blob service.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // List all the containers in the storage account
                BlobContinuationToken containerToken = null;
                List<CloudBlobContainer> containers = new List<CloudBlobContainer>();
                do
                {
                    ContainerResultSegment resultSegment = await blobClient.ListContainersSegmentedAsync(containerToken);
                    containerToken = resultSegment.ContinuationToken;
                    containers.AddRange(resultSegment.Results);
                } while (containerToken != null);

                // Iterate through the containers and list all the blobs in each container
                foreach (CloudBlobContainer container in containers)
                {
                   // Console.WriteLine("Container: " + container.Name);
                    foreach (CloudBlobContainer ImageOrEvent in containers)
                    {
                       // Console.WriteLine("Container: " + container1.Properties.LastModified);
                        if (ImageOrEvent.Properties.LastModified > a)
                        {
                            Console.WriteLine("Container: " + ImageOrEvent);



                        }
                    }

                    //    var ContainerWithTime = container.Properties.LastModified;

                  

                    //BlobContinuationToken blobToken = null;
                    //List<IListBlobItem> blobs = new List<IListBlobItem>();
                    ////do
                    ////{
                    //BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(blobToken);
                    ////blobToken = resultSegment.ContinuationToken;
                    //blobs.AddRange(resultSegment.Results);
                    //Console.WriteLine("///////", resultSegment.Results.ToString());
                    ////    } while (blobToken != null);
                    ////}








                    //// Retrieve storage account information from connection string
                    //// How to create a storage connection string - https://azure.microsoft.com/en-us/documentation/articles/storage-configure-connection-string/
                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=picturesblobstorage;AccountKey=yCk4Z5pshM/1FTn/PRoMLTveIa4NAX2EKju8pmsZccZALtcFZTGoQlrfZFoJureoAU0aXm1VP9Ah+AStO+23gw==;EndpointSuffix=core.windows.net;");

                    //// Create a blob client for interacting with the blob service.
                    //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    //// Get a reference to the container
                    //CloudBlobContainer container = blobClient.GetContainerReference("Blob Containers");

                    //// List all the blobs in the container
                    //BlobContinuationToken continuationToken = null;
                    //List<IListBlobItem> blobs = new List<IListBlobItem>();
                    //do
                    //{
                    //    BlobResultSegment resultSegment = await container.ListBlobsSegmentedAsync(continuationToken);
                    //    continuationToken = resultSegment.ContinuationToken;
                    //    blobs.AddRange(resultSegment.Results);
                    //} while (continuationToken != null);

                    //// Iterate through the blobs and print their names
                    //foreach (var blob in blobs)
                    //{
                    //    Console.WriteLine(blob.Uri);
                    //}



                    //// Iterate through the blobs and print their names
                    //foreach (var blob in blobs)
                    //{
                    //    Console.WriteLine("  " + blob.Uri);
                    //}



                    //string blobName = "image.jpg";
                    //string filePath = "C:/Users/PC/Desktop/New folder (16)";

                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");

                    //// Create the blob client.
                    //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    //// Retrieve a reference to a container.
                    //CloudBlobContainer container = blobClient.GetContainerReference("my-container");
                    //await  container.CreateIfNotExistsAsync();
                    //CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
                    //blockBlob.UploadFromFileAsync(filePath);
                }

            }
        }
    }
}