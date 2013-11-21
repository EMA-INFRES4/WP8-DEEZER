using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using deezer_objects;
using System.Windows.Threading;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System.Windows.Controls;

namespace UI.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public static ProgressBar progress = null;

        private Deezer dz = new Deezer();

        private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();

        private ObservableCollection<Track> _tracksFinded = new ObservableCollection<Track>();


        public ObservableCollection<Track> Tracks
        {
            get { return _tracks; }
            set
            {
                if (_tracks != value)
                {
                    _tracks = value;
                    RaisePropertyChanged(() => Tracks);
                }
            }
        }

        public ObservableCollection<Track> TracksFinded
        {
            get { return _tracksFinded; }
            set
            {
                if (_tracksFinded != value)
                {
                    _tracksFinded = value;
                    RaisePropertyChanged(() => TracksFinded);
                }
            }
        }
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                var list = new ObservableCollection<Track>();
                list.Add(new Track { Title = "title", Artist = new Artist { Name = "artist" }, Album = new Album { Cover = "http://imworld.aufeminin.com/story/20130224/eva-longoria-12883_w1000.jpg" } });
                list.Add(new Track { Title = "title", Artist = new Artist { Name = "artist" }, Album = new Album { Cover = "http://imworld.aufeminin.com/story/20130224/eva-longoria-12883_w1000.jpg" } });
                list.Add(new Track { Title = "title", Artist = new Artist { Name = "artist" }, Album = new Album { Cover = "http://imworld.aufeminin.com/story/20130224/eva-longoria-12883_w1000.jpg" } });
                list.Add(new Track { Title = "title", Artist = new Artist { Name = "artist" }, Album = new Album { Cover = "http://imworld.aufeminin.com/story/20130224/eva-longoria-12883_w1000.jpg" } });

                TracksFinded = list;
            }
        }

        private string _recherche = "Getta";

        public string Recherche
        {
            get
            {
                return _recherche;
            }
            set
            {
                if (_recherche != value)
                {
                    _recherche = value;
                    RaisePropertyChanged(() => Recherche);
                }
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    RaisePropertyChanged(() => IsBusy);
                }
            }
        }


        private ICommand _pageLoaded;
        public ICommand PageLoaded
        {
            get
            {
                if (_pageLoaded == null)
                {
                    _pageLoaded = new RelayCommand(() =>
                    {
                        IsBusy = true;
                        dz.getTopTracks(6, (tr) =>
                        {
                            IsBusy = false;
                            Tracks = tr.Tracks.Data.ToObservableCollection();
                        });
                    });
                }
                return _pageLoaded;
            }
        }

        private ICommand _rechercher;
        public ICommand RechercherCommand
        {
            get
            {
                if (_rechercher == null)
                {
                    _rechercher = new RelayCommand(() =>
                    {
                        IsBusy = true;
                        dz.findTracks(Recherche, (tr) => RechercheCommand_Finished(tr));
                    });
                }
                return _rechercher;
            }
        }
        private void RechercheCommand_Finished(TracksResponse tr)
        {
            IsBusy = false;
            TracksFinded = tr.Tracks.Data.ToObservableCollection();
        }
    }
}