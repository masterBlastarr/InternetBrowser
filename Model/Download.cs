using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace Player
{
    public class Download : INotifyPropertyChanged
    {
        CoreDispatcher Dispatcher;
        public Download(CoreDispatcher disp)
        {
            Dispatcher = disp;
        }
        public static ObservableCollection<Download> DownloadList = new ObservableCollection<Download>();
        public Uri URL { get; set; }
        public string Filename { get; set; }
         ulong downloaded;
        public ulong Downloaded
        {
            get
            {
                return downloaded;
            }
            set
            {
                downloaded = value;
                RaisePropertyChangedAsync("Downloaded");
            }
        }
         long fileSize;
        public long FileSize
        {
            get
            {
                return fileSize;
            }
            set
            {
                fileSize = value;
                RaisePropertyChangedAsync("FileSize");
            }
        }
        public int Unit { get; set; }
         double percentDownload;
        public double PercentDownload
        {
            get
            {
                return percentDownload;
            }
            set
            {
                percentDownload = value;
                RaisePropertyChangedAsync("PercentDownload");
            }
        }
        public DateTime TimeToEnd { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected async Task RaisePropertyChangedAsync(string name)
        {
            if (PropertyChanged != null)
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
                });
            }
        }
    }
}
