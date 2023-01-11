using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

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
            var Time = DateTimeOffset.UtcNow;
            Console.WriteLine($"the time is *****************{Time}");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                await Task.Delay(5000);
                string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                AsyncPageable<BlobContainerItem> containers = blobServiceClient.GetBlobContainersAsync();
                await foreach (BlobContainerItem container in containers)
                {
                    if (container.Properties.LastModified > Time)
                    {
                        Console.WriteLine("Container: " + container.Name);
                    }
                }
            }
        }
     }
 }
