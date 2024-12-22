using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.Services;
using System.Collections.ObjectModel;

namespace disaster_management.ViewModels.ChildViewModels
{

    public class DiseaseViewModel : ObservableObject
    {
        private readonly IDiseaseService? _diseaseService;

        public DiseaseViewModel(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;
            LoadDiseasesCommand = new AsyncRelayCommand(LoadDiseasesAsync);
        }

        #region Prop
        private ObservableCollection<DiseaseType>? _diseases;
        public ObservableCollection<DiseaseType>? Diseases
        {
            get => _diseases;
            set => SetProperty(ref _diseases, value);
        }

        private string _diseaseName;
        public string DiseaseName
        {
            get { return _diseaseName; }
            set => SetProperty(ref _diseaseName, value);
        }
        

        private DiseaseType _SelectedDisease;
        public DiseaseType SelectedDisease
        {
            get { return _SelectedDisease; }
            set => SetProperty(ref _SelectedDisease, value);
        }


        #endregion Prop

        public IAsyncRelayCommand LoadDiseasesCommand { get; }

        private async Task LoadDiseasesAsync()
        {
            if (_diseaseService is null)
            {
                return;
            }
            var diseases = await _diseaseService.GetAllAsync();
            Diseases = new ObservableCollection<DiseaseType>(diseases);
        }
       
    }
}
