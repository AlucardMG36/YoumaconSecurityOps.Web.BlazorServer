using Azure.Core.Extensions;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

namespace YsecOps.UI.Extensions;

internal static class AzureClientFactoryBuilderExtensions
{
    public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
    => preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out var serviceUri)
            ? builder.AddBlobServiceClient(serviceUri)
            : builder.AddBlobServiceClient(serviceUriOrConnectionString);

    public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClient(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString, bool preferMsi)
    => preferMsi && Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out var serviceUri)
            ? builder.AddQueueServiceClient(serviceUri)
            : builder.AddQueueServiceClient(serviceUriOrConnectionString);
}
