using CommunityToolkit.Mvvm.ComponentModel;
using disaster_management.ViewModels.ChildViewModels;

namespace disaster_management.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        // private readonly IServiceProvider _serviceProvider;
        public DiseaseViewModel DiseaseViewModel { get; }
        public LiveStockViewModel LiveStockViewModel { get; }
        public MainViewModel(DiseaseViewModel diseaseViewModel, LiveStockViewModel liveStockViewModel)
        {
            DiseaseViewModel = diseaseViewModel;
            LiveStockViewModel = liveStockViewModel;
        }

        // Lazy load if you like

        //  Lazy Initialization cho DiseaseViewModel
        //private DiseaseViewModel? _diseaseViewModel;
        //public DiseaseViewModel? DiseaseViewModel =>
        //    _diseaseViewModel ??= _serviceProvider.GetRequiredService<DiseaseViewModel>();

        ////  Lazy Initialization cho LiveStockViewModel
        //private LiveStockViewModel? _liveStockViewModel;
        //public LiveStockViewModel? LiveStockViewModel =>
        //    _liveStockViewModel ??= _serviceProvider.GetRequiredService<LiveStockViewModel>();
    }
}
