using CSGO.Validators;
using Xamarin.Forms;

namespace CSGO.ViewModels
{
    public class PlayerDetailsViewModel : BaseViewModel
    {
        private int _playerId;
        public int PlayerId
        {
            get => _playerId;
            set { _playerId = value; OnPropertyChanged(nameof(PlayerId)); }
        }

        private string _steamProfile;
        public string SteamProfile
        {
            get => _steamProfile;
            set
            {
                if ((value != null && value.Length <= 55) || value == null)
                    _steamProfile = value;
                OnPropertyChanged(nameof(SteamProfile));
            }
        }

        private string _teamName;
        public string TeamName
        {
            get
            {
                //if (_teamName.Length > 15)
                //    return _teamName.Substring(0, 15) + "...";
                return _teamName;
            }
            set { _teamName = value; OnPropertyChanged(nameof(TeamName)); }
        }

        public Command DeletePlayer
        {
            get
            {
                return new Command(async () =>
                {
                    var answer = await Application.Current.MainPage.DisplayAlert("Deleting", "Are you sure you want to delete this player?", "Yes", "No");
                    if (answer)
                    {
                        await App.PlayerRepository.DeletePlayerAsync(PlayerId);
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }
                });
            }
        }

        public Command UpdatePlayer
        {
            get
            {
                return new Command(async () =>
                {
                    if (Validator.ValidatePlayer(SteamProfile))
                    {
                        var player = await App.PlayerRepository.GetPlayerByIdAsync(PlayerId);
                        player.SteamProfile = SteamProfile;
                        var result = await App.PlayerRepository.UpdatePlayerAsync(player);
                        if (result)
                            await Application.Current.MainPage.DisplayAlert("Success!", "Player has been updated!", "Ok");
                        else
                            await Application.Current.MainPage.DisplayAlert("Error!", "Could not update player", "Ok");
                    }
                    else
                        await Application.Current.MainPage.DisplayAlert("Error!", "Player Steam profile is invalid!", "Ok");
                });
            }
        }
    }
}
