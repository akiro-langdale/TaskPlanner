namespace TaskPlanner.App
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Windows;
    using TaskPlanner.App.Tasks;
    using TaskPlanner.App.Utils;
    using TaskPlanner.App.ViewModels;
    using TaskPlanner.App.Views.Windows;
    using TaskPlanner.Data.DBContext;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<TaskPlannerDBContext>();

                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<BDViewerVeiwModel>();
                    services.AddSingleton<NavigationTask>();
                    services.AddSingleton<DataTask>();

                    services.AddTransient<AddEditUserWindow>();
                    services.AddTransient<AddEditTaskWindow>();
                    services.AddTransient<AddEditTaskAssignmentWindow>();

                    services.AddTransient<MainWindowViewModel>();
                    services.AddTransient<UsersListViewModel>();
                    services.AddTransient<TasksListViewModel>();
                    services.AddTransient<TaskAssignmentsListViewModel>();
                })
                .Build();
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
            _host.Dispose();
            base.OnExit(e);
        }

        private async void AppStartup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();

            ServiceLocator.SetLocatorProvider(_host.Services);

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();
        }
    }

}
