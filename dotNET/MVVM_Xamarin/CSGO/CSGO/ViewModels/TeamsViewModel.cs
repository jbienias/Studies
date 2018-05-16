using CSGO.Models;
using CSGO.Validators;
using CSGO.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CSGO.ViewModels
{
    public class TeamsViewModel : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if ((value != null && value.Length <= 20) || value == null)
                {
                    _name = value;
                }; OnPropertyChanged(nameof(Name));
            }
        }

        private string _logoUrl;
        public string LogoUrl
        {
            get => _logoUrl;
            set { _logoUrl = value; OnPropertyChanged(nameof(LogoUrl)); }
        }

        private ObservableCollection<Team> _teams;
        public ObservableCollection<Team> Teams
        {
            get => _teams;
            set { _teams = value; OnPropertyChanged(nameof(Teams)); }
        }

        private Team _selectedTeam;
        public Team SelectedTeam
        {
            get => _selectedTeam;
            set
            {
                _selectedTeam = value;
                if (_selectedTeam == null)
                    return;
                SelectTeam.Execute(_selectedTeam);
                _selectedTeam = null;
                OnPropertyChanged(nameof(SelectedTeam));
            }
        }

        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(nameof(IsVisible)); }
        }

        public Command AddTeam
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidateTeam(Name))
                    {
                        var team = new Team
                        {
                            Name = Name,
                            LogoUrl = LogoUrl
                        };
                        await App.TeamRepository.AddTeamAsync(team);
                        RefreshTeams.Execute(null);
                        Name = String.Empty;
                        LogoUrl = String.Empty;
                        VisibilitySwitchCommand.Execute(null);
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Team name is invalid!", "Ok");
                });
            }
        }

        public Command SelectTeam
        {
            get
            {
                return new Command(async () =>
                {
                    var teamDetailsViewModel = new TeamDetailsViewModel()
                    {
                        TeamId = SelectedTeam.Id,
                        LogoUrl = SelectedTeam.LogoUrl,
                        Name = SelectedTeam.Name,
                        ActualTeamName = SelectedTeam.Name,
                        ActualTeamLogoUrl = SelectedTeam.LogoUrl
                    };
                    var teamDetailsPage = new TeamDetailsPage(teamDetailsViewModel);
                    await Application.Current.MainPage.Navigation.PushAsync(teamDetailsPage);
                });
            }
        }

        public Command RefreshTeams
        {
            get
            {
                return new Command(async () =>
                {
                    var teams = await App.TeamRepository.GetTeamsAsync();
                    Teams = new ObservableCollection<Team>(teams);
                });
            }
        }

        public Command VisibilitySwitchCommand
        {
            get { return new Command(() => { IsVisible = !IsVisible; }); }
        }
    }
}
