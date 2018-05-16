using Xamarin.Forms;
using CSGO.Data;
using CSGO.Helpers;
using CSGO.ViewModels;
using CSGO.Views;

namespace CSGO
{
    public partial class App : Application
    {
        private static ITeamRepository _teamRepository;
        private static IPlayerRepository _playerRepository;
        private static readonly string _dbName = "CSGO5.db";

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TeamsPage(new TeamsViewModel()));
        }

        public static ITeamRepository TeamRepository
        {
            get
            {
                if (_teamRepository == null)
                {
                    var dbPath = DependencyService.Get<ILocalFileHelper>().GetLocalFilePath(_dbName);
                    var dbContext = new DatabaseContext(dbPath);
                    _teamRepository = new TeamRepository(dbContext);
                }
                return _teamRepository;
            }
        }

        public static IPlayerRepository PlayerRepository
        {
            get
            {
                if (_playerRepository == null)
                {
                    var dbPath = DependencyService.Get<ILocalFileHelper>().GetLocalFilePath(_dbName);
                    var dbContext = new DatabaseContext(dbPath);
                    _playerRepository = new PlayerRepository(dbContext);
                }
                return _playerRepository;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
