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
using DealBox.ViewModel;

namespace DealBox
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            App.mainViewModel = new MainViewModel();
            this.DataContext = App.mainViewModel;
            App.mainViewModel.GetFeeds();
        }

        private void feedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex == -1)
                return;

            App.mainViewModel.SelectedFeedIndex = (sender as ListBox).SelectedIndex;
            NavigationService.Navigate(new Uri("/DealDetail.xaml", UriKind.RelativeOrAbsolute));
            (sender as ListBox).SelectedIndex = -1;
        }
    }
}