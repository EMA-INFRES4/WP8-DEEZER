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
using UI.ViewModel;

namespace UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BackgroundAudioPlayer.Instance.PlayStateChanged += Instance_PlayStateChanged;
            MainViewModel.progress = Progress;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("coucou :)");
        }
        
        private void LBTracks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lstBox = (ListBox)sender;
            Track track = (Track)lstBox.SelectedItem;
            AudioTrack curTrack = new AudioTrack(
                new Uri(track.Preview, UriKind.Absolute),
                track.Title,
                track.Artist.Name,
                track.Album.Title,
                new Uri(track.Album.Cover,UriKind.Absolute)
            );
            BackgroundAudioPlayer.Instance.Track = curTrack;
            BackgroundAudioPlayer.Instance.Volume = 1;
            BackgroundAudioPlayer.Instance.Stop();
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
                System.Diagnostics.Debug.WriteLine(BackgroundAudioPlayer.Instance.Track.Title);
            }
        
        }
    }
}
