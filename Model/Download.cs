using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    public class Download : INotifyPropertyChanged
    {
        public static ObservableCollection<Download> DownloadList = new ObservableCollection<Download>();
        public Uri URL { get; set; }
        public string Filename { get; set; }
        public ulong downloaded;
        public ulong Downloaded
        {
            get
            {
                return downloaded;
            }
            set
            {
                downloaded = value;
                RaisePropertyChanged("Downloaded");
            }
        }
        public ulong FileSize { get; set; }
        public int Unit { get; set; }
        public ulong percentDownload;
        public ulong PercentDownload
        {
            get
            {
                return percentDownload;
            }
            set
            {
                percentDownload = value;
                RaisePropertyChanged("PercentDownload");
            }
        }
        public DateTime TimeToEnd { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
