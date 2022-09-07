using Downloader;

var urlToDownload = "https://freetestdata.com/wp-content/uploads/2022/02/Free_Test_Data_10MB_MP4.mp4";
var filePath = @"test1.m4a";

var downloader = new DownloadService();

int lastProgress = -1;
downloader.ChunkDownloadProgressChanged += (s, e) =>
{
    if (lastProgress != (int)e.ProgressPercentage)
    {
        Console.WriteLine($"Progress {(int)e.ProgressPercentage}% {e.BytesPerSecondSpeed}");
    }
    lastProgress = (int)e.ProgressPercentage;
};

try
{
    Task.Run(() => downloader.DownloadFileTaskAsync(urlToDownload, filePath)); //start
    Console.WriteLine("started");
 
    await Task.Delay(2000);
    Console.WriteLine("delay");

    var package = downloader.Package;
    Console.WriteLine($"IsSupportDownloadInRange: {package.IsSupportDownloadInRange}");

    downloader.CancelAsync(); //pause
    Console.WriteLine("cancelled");

    //await Task.Delay(1000); //'fix' issue

    using var stream = await downloader.DownloadFileTaskAsync(package); //resume
    Console.WriteLine("resumed");
}
catch (Exception ex)
{

}