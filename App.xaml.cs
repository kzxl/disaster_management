using System.Configuration;
using System.Data;
using System.Windows;
using disaster_management.Models;
using disaster_management.Repositories;
using disaster_management.Services;
using disaster_management.ViewModels;
using disaster_management.ViewModels.ChildViewModels;
using disaster_management.Views.Usercontrols;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace disaster_management
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceProvider = ConfigureServices();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
            base.OnExit(e);
        }
        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DaDManagementContext>(options =>
                options.UseSqlServer("Server=DESKTOP-JMJDD80\\SQLEXPRESS;Database=DaDManagement;User Id=sa;Password=Admin@1234;"));

            // Đăng ký Repository
            services.AddScoped<DiseaseRepository>();


            // Đăng ký các Service
            services.AddScoped<IDiseaseService, DiseaseService>();

            // Đăng ký UnitOfWork
            //  services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddTransient<DiseaseViewModel>();
    

            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
    

            return services.BuildServiceProvider();
        }

    }
}


