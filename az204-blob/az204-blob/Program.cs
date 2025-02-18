using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Identity;




Console.WriteLine("Azure blob storage exervise \n");

// Run the examples asynchronously, wait for the results beore proceeding
ProcessAsync().GetAwaiter().GetResult();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

static async Task ProcessAsync()
{
    // uncomment and add connection string, avaoiding github push protection
    //string storageConnectionString = 
    // create a client that can authenticate with connection string;
    BlobServiceClient serviceClient = new BlobServiceClient(storageConnectionString);

    // create a unique name for the container 
    string containerName = "xyzblob" + Guid.NewGuid().ToString();

    // create the contaner and return a container client object
    BlobContainerClient containerClient = await serviceClient.CreateBlobContainerAsync(containerName);
    Console.WriteLine($"A contaner named  ${containerClient} has been created");
    Console.WriteLine("\nTake a minute and verify in the portal." +
    "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press Enter to Continue");
    Console.ReadLine();

    // Upload blobs to a container 
    string localPath = "./data/";
    string fileName = "wtffile" + Guid.NewGuid().ToString() + ".txt";
    string localFilePath = Path.Combine(localPath, fileName);

    // Write text to the file 
    await File.WriteAllTextAsync(localFilePath, "Hello World");

    // Get a reference to the blob
    BlobClient blobClient = containerClient.GetBlobClient(fileName);

    Console.WriteLine("Uploading to the blog storage blob:\n\t {0}\n", blobClient.Uri);

    // Open the file and upload its data
    using (FileStream uploadFileStream = File.OpenRead(localFilePath))
    {
        await blobClient.UploadAsync(uploadFileStream);
        uploadFileStream.Close();
    }

    Console.WriteLine("\nThe file was uploaded. We'll verify by listing" +
        " the blobs next.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // List Blob in the container 

    Console.WriteLine("Listing blobs ...");
    await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
    {
        Console.WriteLine("\t" + blobItem.Name);
    }

    Console.WriteLine("\n You can verify by looing inside the container in the portal ");
    Console.WriteLine("Please 'Enter' to Continue.");
    Console.ReadLine();


    // Download Blobs

    string downloadFilePath = localFilePath.Replace(".txt", "Downloaded.txt");
    Console.WriteLine("\nDobloading blobt to\n\t{0}", downloadFilePath);

    // Download the blob's content and save it to a file
    BlobDownloadInfo download = await blobClient.DownloadAsync();

    using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
    {
        await download.Content.CopyToAsync(downloadFileStream);
    }

    Console.WriteLine("\nLocate the local file in the data directory created earlier to verify it was downloaded.");
    Console.WriteLine("The next step is to delete the container and local files.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

    // Delete Container 
    Console.WriteLine("\n Deleting blob container");
    await containerClient.DeleteAsync();

    //Delete Local source and downloaded files;
    File.Delete(localFilePath);
    File.Delete(downloadFilePath);

    Console.WriteLine("Finished CLeaning up");

}

/*

namespace azblob
{

    public class BlobServices
    {


        public BlobServiceClient GetBlobServiceClient(string accountName)
        {
            BlobServiceClient blobServiceClient = new(
                new Uri($"https://${accountName}.blob.core.windows.net"),
                new DefaultAzureCredential()
                );
            return blobServiceClient;

        }

        public BlobContainerClient GetBlobContainerClient(string containerName, BlobServiceClient client)
        {

            BlobContainerClient containerClient = client.GetBlobContainerClient(containerName);
            return containerClient;
        }

        public BlobClient GetBlobClient(BlobServiceClient blobServiceClient, string containerName, string blobName)
        {
            BlobClient client = blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
            return client;
        }

        public static void Main()
        {
            string accountName = "";
            string containerName = "";
            string blobName = "";

            BlobServiceClient blobServiceClient = GetBlobServiceClient(accountName);

        }


    }

}

*/