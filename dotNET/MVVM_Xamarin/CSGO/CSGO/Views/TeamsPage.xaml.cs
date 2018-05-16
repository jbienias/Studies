using CSGO.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSGO.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamsPage : ContentPage
    {
        public TeamsPage(TeamsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            var viewModel = this.BindingContext as TeamsViewModel;
            if (viewModel == null)
                return;

            viewModel.RefreshTeams.Execute(null);
        }
    }
}