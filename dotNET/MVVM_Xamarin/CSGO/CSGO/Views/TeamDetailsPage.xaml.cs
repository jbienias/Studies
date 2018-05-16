using CSGO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSGO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetailsPage : ContentPage
    {
        public TeamDetailsPage(TeamDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = this.BindingContext as TeamDetailsViewModel;
            if (viewModel == null)
                return;

            viewModel.RefreshPlayers.Execute(null);
        }
    }
}