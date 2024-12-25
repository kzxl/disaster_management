using System;
using System.Configuration;
using System.Data;
using System.Windows;
using disaster_management.Models;
using disaster_management.Repositories.Disease;
using disaster_management.Repositories.Livestock;
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

            // Check database connection
            if (!TestDatabaseConnection())
            {
                MessageBox.Show("Failed to connect to the database. Please check your connection settings.", "Connection Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

         
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
        private bool TestDatabaseConnection()
        {
            try
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DaDManagementContext>();
                    context.Database.OpenConnection();
                    context.Database.CloseConnection();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                Console.WriteLine($"Database connection test failed: {ex.Message}");
                return false;
            }
        }
        private ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Servername 1: NGUYENMINHCHAUM\SQLEXPRESS pass Admin@1234
            // Servername 2: MYPC\SQLEXPRESS pass 123456
            services.AddDbContext<DaDManagementContext>(options =>
                options.UseSqlServer("Server=NGUYENMINHCHAUM\\SQLEXPRESS;Database=DaDManagement;User Id=sa;Password=Admin@1234;"));

            // Register Repository
            //DiseaseRepository
            services.AddSingleton<DiseaseRepository>();
            services.AddSingleton<OutbreakDiagnosisRepository>();
            services.AddSingleton<OutbreakRepository>();
            services.AddSingleton<SymptomRepository>();
            services.AddSingleton<VaccinationRepository>();

            //LivestockRepository
            services.AddSingleton<CertificateRepository>();
            services.AddSingleton<LivestockFarmRepository>();
            services.AddSingleton<LivestockFarmConditionRepository>();
            services.AddSingleton<SlaughterhouseRepository>();
            services.AddSingleton<SafeLivestockZoneRepository>();
            services.AddSingleton<LivestockStatisticRepository>();
            services.AddSingleton<TemporaryZoneRepository>();
            services.AddSingleton<VeterinaryBranchRepository>();
            services.AddSingleton<VetMedicineAgencyRepository>();
           


            // Register for Services
            //DiseaseService
            services.AddSingleton<IDiseaseTypeService, DiseaseService>();
            services.AddSingleton<IOutbreakDiagnosisService, DiseaseService>();
            services.AddSingleton<IOutbreakService, DiseaseService>();
            services.AddSingleton<ISymptomService, DiseaseService>();
            services.AddSingleton<IVaccinationService, DiseaseService>();

            //LivestockService
            services.AddSingleton<ICertificateService, LivestockService>();
            services.AddSingleton<ILivestockFarmService, LivestockService>();
            services.AddSingleton<ILivestockFarmConditionService, LivestockService>();
            services.AddSingleton<ISlaughterhouseService, LivestockService>();
            services.AddSingleton<ISafeLivestockZoneService, LivestockService>();
            services.AddSingleton<ILivestockStatisticService, LivestockService>();
            services.AddSingleton<ITemporaryZoneService, LivestockService>();
            services.AddSingleton<IVeterinaryBranchService, LivestockService>();
            services.AddSingleton<IVetMedicineAgencyService, LivestockService>();


            //// Viewmodel registration
            services.AddSingleton<DiseaseViewModel>();
            services.AddSingleton<LiveStockViewModel>();
           
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();

            return services.BuildServiceProvider();
        }

    }
}


