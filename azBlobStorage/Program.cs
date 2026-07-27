using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var credential = new DefaultAzureCredential();
string? accountName = config["StorageAccountName"];

Console.WriteLine("Azure Blob Storage Lab\n");

// Run the examples asynchronously, wait for the results before proceeding
await ProcessAsync();

Console.WriteLine("\nPress enter to exit the sample application.");
Console.ReadLine();

async Task ProcessAsync()
{
  // CREATE A BLOB STORAGE CLIENT
  // Create the BlobServiceClient using the endpoint and DefaultAzureCredential
  string blobServiceEndpoint = $"https://{accountName}.blob.core.windows.net";
  BlobServiceClient blobServiceClient = new(new Uri(blobServiceEndpoint), credential);


  // CREATE A CONTAINER
  // Create a unique name for the container
  string containerName = "wtblob" + Guid.NewGuid().ToString();

  // Create the container and return a container client object
  Console.WriteLine("Creating container: " + containerName);
  BlobContainerClient containerClient =
      await blobServiceClient.CreateBlobContainerAsync(containerName);

  // Check if the container was created successfully
  if (containerClient != null)
  {
    Console.WriteLine("Container created successfully, press 'Enter' to continue.");
    Console.ReadLine();
  }
  else
  {
    Console.WriteLine("Failed to create the container, exiting program.");
    return;
  }

  // CREATE A LOCAL FILE FOR UPLOAD TO BLOB STORAGE
  Console.WriteLine("Creating a local file for upload to Blob storage...");
  string localPath = "./data/";
  string fileName = "wtfile" + Guid.NewGuid().ToString() + ".txt";
  string localFilePath = Path.Combine(localPath, fileName);

  // Write text to the file
  await File.WriteAllTextAsync(localFilePath, "Hello, World!");
  Console.WriteLine("Local file created, press 'Enter' to continue.");
  Console.ReadLine();

  // UPLOAD THE FILE TO BLOB STORAGE
  BlobClient blobClient = containerClient.GetBlobClient(fileName);

  Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}", blobClient.Uri);

  // Open the file and upload its data
  using (FileStream uploadFileStream = File.OpenRead(localFilePath))
  {
    await blobClient.UploadAsync(uploadFileStream);
    uploadFileStream.Close();
  }

  // Verify if the file was uploaded successfully
  bool blobExists = await blobClient.ExistsAsync();
  if (blobExists)
  {
    Console.WriteLine("File uploaded successfully, press 'Enter' to continue.");
    Console.ReadLine();
  }
  else
  {
    Console.WriteLine("File upload failed, exiting program..");
    return;
  }

  // LIST BLOBS IN THE CONTAINER
  Console.WriteLine("Listing blobs in container...");
  await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
  {
    Console.WriteLine("\t" + blobItem.Name);
  }

  Console.WriteLine("Press 'Enter' to continue.");
  Console.ReadLine();

  // DOWNLOAD THE BLOB TO A LOCAL FILE
  string downloadFilePath = localFilePath.Replace(".txt", "DOWNLOADED.txt");

  Console.WriteLine("Downloading blob to: {0}", downloadFilePath);

  // Download the blob's contents and save it to a file
  BlobDownloadInfo download = await blobClient.DownloadAsync();

  using (FileStream downloadFileStream = File.OpenWrite(downloadFilePath))
  {
    await download.Content.CopyToAsync(downloadFileStream);
  }

  Console.WriteLine("Blob downloaded successfully to: {0}", downloadFilePath);
}
