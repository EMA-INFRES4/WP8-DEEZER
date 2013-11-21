using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using deezer_objects;

namespace EMA_DEEZER
{
    public partial class MainPage : PhoneApplicationPage
    {
        Deezer dz;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            dz = new Deezer();
            dz.getTopTracks(6, (x) => SetListTracks(x));
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
        private void SetListTracks(TracksResponse tr)
        {
            ListSongs.ItemsSource = tr.tracks;
        }
    }
}