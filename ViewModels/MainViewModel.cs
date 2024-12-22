using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using disaster_management.ViewModels.ChildViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace disaster_management.ViewModels
{
    public class MainViewModel : ObservableObject
    {
 
        private readonly IServiceProvider _serviceProvider;
        public MainViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            TriggerChildCommand = new RelayCommand(ExecuteChildCommand);
        }

      //  Lazy Initialization cho DiseaseViewModel
        private DiseaseViewModel? _diseaseViewModel;
        public DiseaseViewModel? DiseaseViewModel =>
            _diseaseViewModel ??= _serviceProvider.GetRequiredService<DiseaseViewModel>();




        // Command để thao tác với DiseaseViewModel
        public RelayCommand TriggerChildCommand { get; }

        private void ExecuteChildCommand()
        {
            DiseaseViewModel?.LoadDiseasesCommand.Execute(null);
        }

    }
}
