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
using System.Net.NetworkInformation;

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

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IsProgressBarVisible = Visibility.Visible;
                feedRequest.DownloadStringAsync(new Uri("http://www.desidime.com/premium_deals.atom"));
            }
            else
            {
                MessageBox.Show("No Network Available.","Network Error",MessageBoxButton.OK);
            }
        }

        void feedRequest_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            IsProgressBarVisible = Visibility.Collapsed;

            try
            {
                XDocument document = XDocument.Parse(e.Result);
                XmlSerializer serializer = new XmlSerializer(typeof(feed));
                feed feed = (feed)serializer.Deserialize(document.CreateReader());
                FeedList = new List<feedEntry>(feed.entry);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, An error occured while connecting to Server.","Server Error",MessageBoxButton.OK);
            }
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
