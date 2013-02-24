using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace DealBox.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged 
    {
        private WebClient feedRequest;
        public int SelectedFeedIndex = -1;
        private List<feedEntry> feedList;
        public List<feedEntry> FeedList
        {
            get
            {
                return feedList;
            }
            set
            {
                feedList = value;
                NotifyPropertyChanged("FeedList");
            }
        }

        private Visibility isProgressBarVisible;
        public Visibility IsProgressBarVisible
        {
            get
            {
                return isProgressBarVisible;
            }
            set
            {
                isProgressBarVisible = value;
                NotifyPropertyChanged("IsProgressBarVisible");
            }
        }

        
        public MainViewModel()
        {
            
        }

        public void GetFeeds()
        {
            feedRequest = new WebClient();
            feedRequest.DownloadStringCompleted += new DownloadStringCompletedEventHandler(feedRequest_DownloadStringCompleted);

            IsProgressBarVisible = Visibility.Visible;
            feedRequest.DownloadStringAsync(new Uri("http://www.desidime.com/premium_deals.atom"));
        }

        void feedRequest_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            IsProgressBarVisible = Visibility.Collapsed;
            XDocument document = XDocument.Parse(e.Result);
            XmlSerializer serializer = new XmlSerializer(typeof(feed));
            feed feed = (feed)serializer.Deserialize(document.CreateReader());
            FeedList = new List<feedEntry>(feed.entry);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public feedEntry GetSelectedFeed()
        {
            return FeedList[SelectedFeedIndex];
        }
    }
}
