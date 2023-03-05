namespace CarsIsland.WebApp.Data;

public class BlobConfiguration
{
    public string BlobServerAddress { get; }

    public BlobConfiguration(string blobServerAddress)
    {
        BlobServerAddress = blobServerAddress;
    }
}