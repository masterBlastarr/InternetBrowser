using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Xaml.Controls;

namespace Player
{
    class DownloadScript
    {
        Download DObject;
        async public Task Download(Uri fileUri,Page Card)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            using (WebClient cln = new WebClient())
            {
                string filename = "ŚĆŻ";
                string partfilename = string.Empty;
                string type = string.Empty;
                long Size = 0;
                try
                {
                    cln.OpenRead(fileUri);
                    string header_contentDisposition = cln.ResponseHeaders["content-disposition"];
                     filename = new ContentDisposition(header_contentDisposition).FileName;
                    byte[] bytes = Encoding.Default.GetBytes(filename);
                    filename = Encoding.UTF8.GetString(bytes);
                    partfilename = filename.Substring(0, filename.LastIndexOf('.'));
                        type = filename.Substring(filename.LastIndexOf('.'));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
                
                    FileSavePicker picker = new FileSavePicker();
                    picker.SuggestedFileName=partfilename;
                    picker.FileTypeChoices.Add(type.Substring(1), new List<string>() { type });
                    picker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

                    var file = await picker.PickSaveFileAsync();
                   var DownloadO= new Download(Card.Dispatcher)
                    {
                        Filename=file.Name,
                        URL= fileUri,
                        FileSize=Size
                        
                    };
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == FileUpdateStatus.Complete)
                    {
                        BackgroundDownloader downloader = new BackgroundDownloader();
                        DownloadOperation download = downloader.CreateDownload(DownloadO.URL, file);
                        DObject = DownloadO;
                        Player.Download.DownloadList.Add(DObject);

                        download.StartAsync();
                    download.RangesDownloaded += Download_RangesDownloaded;
                    }
                
            }
        }

        private void Download_RangesDownloaded(DownloadOperation obj, BackgroundTransferRangesDownloadedEventArgs args)
        {
            var index = Player.Download.DownloadList.IndexOf(DObject);
            Player.Download.DownloadList[index].Downloaded = obj.Progress.BytesReceived;
            if (obj.Progress.TotalBytesToReceive != 0)
            {
                Player.Download.DownloadList[index].FileSize =(long) obj.Progress.TotalBytesToReceive;
                Player.Download.DownloadList[index].PercentDownload = obj.Progress.BytesReceived / obj.Progress.TotalBytesToReceive * 100;
            }
        }

        
    }
}
