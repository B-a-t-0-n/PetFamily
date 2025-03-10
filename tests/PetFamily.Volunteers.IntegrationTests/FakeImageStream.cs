namespace PetFamily.Volunteers.IntegrationTests;

public class FakeImageStream : MemoryStream
{
    public FakeImageStream()
    {
        // Create a fake PNG header
        byte[] pngHeader = new byte[]
        {
            0x89, 0x50, 0x4E, 0x47, // PNG signature bytes
            0x0D, 0x0A, 0x1A, 0x0A  // PNG newline and EOF bytes
        };

        // Write the fake PNG header to the stream
        Write(pngHeader, 0, pngHeader.Length);

        // Add some more fake data to simulate an image
        byte[] fakeData = new byte[1024]; // 1KB of fake data
        new Random().NextBytes(fakeData);
        Write(fakeData, 0, fakeData.Length);

        // Reset the position to the beginning of the stream
        Position = 0;
    }

    // Override the Dispose method to prevent the stream from being closed
    protected override void Dispose(bool disposing)
    {
        // Do not close the stream
    }
}