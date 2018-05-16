using CSGO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSGO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerDetailsPage : ContentPage
    {
        public PlayerDetailsPage(PlayerDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}