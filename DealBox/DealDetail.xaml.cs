using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace DealBox
{
    public partial class DealDetail : PhoneApplicationPage
    {
        WebBrowserTask webBrowserTask;
        public DealDetail()
        {
            InitializeComponent();
            this.DataContext = App.mainViewModel.GetSelectedFeed();
            webBrowser1.NavigateToString(App.mainViewModel.GetSelectedFeed().content.Value);
        }

        private void webBrowser1_Navigating(object sender, NavigatingEventArgs e)
        {
            webBrowserTask = new WebBrowserTask();
            webBrowserTask.Uri = e.Uri;
            webBrowserTask.Show();
            e.Cancel = true;
        }

        private void ApplicationBarPreviousButton_Click(object sender, EventArgs e)
        {
            if (App.mainViewModel.SelectedFeedIndex > 0)
            {
                App.mainViewModel.SelectedFeedIndex--;
                this.DataContext = App.mainViewModel.GetSelectedFeed();
                webBrowser1.NavigateToString(App.mainViewModel.GetSelectedFeed().content.Value);
            }
        }

        private void ApplicationBarNextButton_Click(object sender, EventArgs e)
        {
            if (App.mainViewModel.SelectedFeedIndex < App.mainViewModel.FeedList.Count - 1)
            {
                App.mainViewModel.SelectedFeedIndex++;
                this.DataContext = App.mainViewModel.GetSelectedFeed();
                webBrowser1.NavigateToString(App.mainViewModel.GetSelectedFeed().content.Value);
            }
        }

        private void ApplicationBarShareItem_Click(object sender, EventArgs e)
        {
            ShareLinkTask share = new ShareLinkTask();
            share.LinkUri = new Uri("http://desidime.com" + App.mainViewModel.GetSelectedFeed().id);
            share.Title = "Hi Friends, Check out this deal...";
            share.Message = App.mainViewModel.GetSelectedFeed().title;
            share.Show();
        }

        private void ApplicationBarEmailItem_Click(object sender, EventArgs e)
        {
            EmailComposeTask email = new EmailComposeTask();
            email.Subject = "Hi, Check out this deal...";
            email.Body = "Hi, Cheack out this deal..\n\n" + App.mainViewModel.GetSelectedFeed().content.Value + "\n\n" + "http://desidime.com" + App.mainViewModel.GetSelectedFeed().id;
            email.Show();
        }
    }
}