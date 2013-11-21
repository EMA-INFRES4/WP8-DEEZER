using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using UI.Resources;
using deezer_objects;
using Microsoft.Phone.BackgroundAudio;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Resources;

namespace UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            Deezer dz = new Deezer();
            dz.getTopTracks(6, (response) => SetListsDataSource(response));
        }

        private void SetListsDataSource(TracksResponse tr)
        {
            LBTracks.ItemsSource = tr.Tracks.Data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("coucou :)");
        }

        private void LBTracks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Track track = (Track)LBTracks.SelectedItem;
            BackgroundAudioPlayer.Instance.Track = new AudioTrack(
                new Uri(track.Preview),
                track.Title,
                track.Artist.Name,
                track.Album.Title,
                new Uri(track.Album.Cover,UriKind.Absolute)
            );
            BackgroundAudioPlayer.Instance.Volume = 1; 
            BackgroundAudioPlayer.Instance.PlayStateChanged += Instance_PlayStateChanged;
            BackgroundAudioPlayer.Instance.Play();
            System.Diagnostics.Debug.WriteLine(track.Preview);
        }
        void Instance_PlayStateChanged(object sender, EventArgs e)
        {
            switch (BackgroundAudioPlayer.Instance.PlayerState)
            {
                case PlayState.Playing:
                    System.Diagnostics.Debug.WriteLine("Is Playing");
                    break;

                case PlayState.Paused:
                    System.Diagnostics.Debug.WriteLine("Is Paused");
                    break;
                case PlayState.Stopped:
                    System.Diagnostics.Debug.WriteLine("Is Stopped");
                    break;
                case PlayState.Unknown:
                    System.Diagnostics.Debug.WriteLine("????");
                    break;
            }

            if (null != BackgroundAudioPlayer.Instance.Track)
            {
            }
        
        }

        
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}
