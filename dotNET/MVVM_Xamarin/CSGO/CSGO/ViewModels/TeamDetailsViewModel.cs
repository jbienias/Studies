using CSGO.Models;
using CSGO.Validators;
using CSGO.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CSGO.ViewModels
{
    public class TeamDetailsViewModel : BaseViewModel
    {
        public string ActualTeamName { get; set; }
        public string ActualTeamLogoUrl { get; set; }

        private int _teamId;
        public int TeamId
        {
            get => _teamId;
            set { _teamId = value; OnPropertyChanged(nameof(TeamId)); }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if ((value != null && value.Length <= 20) || value == null)
                    _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _logoUrl;
        public string LogoUrl
        {
            get => _logoUrl;
            set { _logoUrl = value; OnPropertyChanged(nameof(LogoUrl)); }
        }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get => _players;
            set { _players = value; OnPropertyChanged(nameof(Players)); }
        }

        public Player _selectedPlayer;
        public Player SelectedPlayer
        {
            get => _selectedPlayer;
            set
            {
                _selectedPlayer = value;
                if (_selectedPlayer == null)
                    return;
                ShowPlayerDetails.Execute(_selectedPlayer);
                _selectedPlayer = null;
                OnPropertyChanged(nameof(SelectedPlayer));
            }
        }

        private string _steamProfile;
        public string SteamProfile
        {
            get => _steamProfile;
            set
            {
                if ((value != null && value.Length <= 25) || value == null)
                    _steamProfile = value;
                OnPropertyChanged(nameof(SteamProfile));
            }
        }

        public Command UpdateTeam
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidateTeam(Name))
                    {
                        var team = await App.TeamRepository.GetTeamByIdAsync(TeamId);
                        team.Name = Name;
                        team.LogoUrl = LogoUrl;
                        var result = await App.TeamRepository.UpdateTeamAsync(team);
                        if (result)
                        {
                            await Application.Current.MainPage.DisplayAlert("Success!", "Team has been updated!", "Ok");
                            ActualTeamLogoUrl = LogoUrl;
                            ActualTeamName = Name;
                        }
                        else
                            await Application.Current.MainPage.DisplayAlert("Error!", "Could not update team", "Ok");
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Team name is invalid", "Ok");

                });
            }
        }

        public Command DeleteTeam
        {
            get
            {
                return new Command(async () =>
                {
                    var answer = await Application.Current.MainPage.DisplayAlert("Deleting", "Are you sure you want to delete this team?", "Yes", "No");
                    if (answer)
                    {
                        await App.TeamRepository.DeleteTeamAsync(TeamId);
                        await Application.Current.MainPage.Navigation.PopToRootAsync();
                    }
                });
            }
        }

        public Command AddPlayer
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidatePlayer(SteamProfile))
                    {
                        var player = new Player()
                        {
                            TeamId = TeamId,
                            SteamProfile = SteamProfile
                        };
                        await App.PlayerRepository.AddPlayerAsync(player);
                        SteamProfile = String.Empty;
                        RefreshPlayers.Execute(null);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Player Steam profile is invalid!", "Ok");
                });
            }
        }

        public Command RefreshPlayers
        {
            get
            {
                return new Command(async () =>
                {
                    var players = await App.PlayerRepository.QueryPlayersAsync(x => x.TeamId == TeamId);
                    Players = new ObservableCollection<Player>(players);
                });
            }
        }

        public Command ShowPlayerDetails
        {
            get
            {
                return new Command(async () =>
                {
                    var playerDetailsViewModel = new PlayerDetailsViewModel()
                    {
                        SteamProfile = SelectedPlayer.SteamProfile,
                        PlayerId = SelectedPlayer.Id,
                        TeamName = ActualTeamName
                    };
                    var playerDetailsPage = new PlayerDetailsPage(playerDetailsViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(playerDetailsPage);
                });
            }
        }
    }
}
