using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace disaster_management.ViewModels.ChildViewModels
{

    public class DiseaseViewModel : ObservableObject
    {
        private readonly IDiseaseService? _diseaseService;

        public DiseaseViewModel(IDiseaseService diseaseService)
        {
            _diseaseService = diseaseService;

             LoadDiseasesCommand = new AsyncRelayCommand(GetAllDiseasesAsync); // First load
            LoadDiseasesCommand.ExecuteAsync(null);

            UpdateDiseaseCommand = new AsyncRelayCommand(UpdateDiseaseAsync);
            AddDiseaseCommand = new AsyncRelayCommand(AddDiseaseAsync);
        }

        #region Prop
        private ObservableCollection<DiseaseType>? _diseases;
        public ObservableCollection<DiseaseType>? Diseases
        {
            get => _diseases;
            set => SetProperty(ref _diseases, value);
        }

        private int _diseaseSeverityIndex;
        public int DiseaseSeverityIndex
        {
            get { return _diseaseSeverityIndex; }
            set
            {
                SetProperty(ref _diseaseSeverityIndex, value);
            }
        }

        private int _diseaseSeverityIndexAdd;
        public int DiseaseSeverityIndexAdd
        {
            get { return _diseaseSeverityIndexAdd; }
            set
            {
                SetProperty(ref _diseaseSeverityIndexAdd, value);
            }
        }

        private int _diseaseGroupIndex;
        public int DiseaseGroupIndex
        {
            get { return _diseaseGroupIndex; }
            set { _diseaseGroupIndex = value; OnPropertyChanged(); }
        }

        private int _diseaseGroupIndexAdd;
        public int DiseaseGroupIndexAdd
        {
            get { return _diseaseGroupIndexAdd; }
            set { _diseaseGroupIndexAdd = value; OnPropertyChanged(); }
        }



        private DiseaseType _SelectedDisease;
        public DiseaseType SelectedDisease
        {
            get { return _SelectedDisease; }
            set
            {
                SetProperty(ref _SelectedDisease, value);
                Disease = value.Clone(); // Create a copy
                GetSeverityIndex(SelectedDisease);
                GetGroupIndex(SelectedDisease);
            }
        }

        private DiseaseType _SelectedDiseaseAdd;
        public DiseaseType SelectedDiseaseAdd
        {
            get { return _SelectedDiseaseAdd; }
            set
            {
                SetProperty(ref _SelectedDiseaseAdd, value);
                Disease = value.Clone(); // Create a copy
             
            }
        }

        private DiseaseType _Disease;
        public DiseaseType Disease
        {
            get { return _Disease; }
            set => SetProperty(ref _Disease, value);
        }


        private void GetSeverityIndex(DiseaseType diseaseType)
        {
            if (_diseaseService is null)
            {
                return;
            }
            if (diseaseType.Severity == "Nhẹ")
            {
                DiseaseSeverityIndex = 0;
            }
            else if (diseaseType.Severity == "Cảnh báo")
            {
                DiseaseSeverityIndex = 1;
            }
            else if (diseaseType.Severity == "Nguy hiểm")
            {
                DiseaseSeverityIndex = 2;
            }
        }

        private void GetGroupIndex(DiseaseType diseaseType)
        {
            if (_diseaseService is null)
            {
                return;
            }
            if (diseaseType.DiseaseGroup == "Virus")
            {
                DiseaseGroupIndex = 0;
            }
            else if (diseaseType.DiseaseGroup == "Vi Khuẩn")
            {
                DiseaseGroupIndex = 1;
            }
            else if (diseaseType.DiseaseGroup == "Ký Sinh")
            {
                DiseaseGroupIndex = 2;
            }
        }

        private void SetSeverity(int index)
        {
            if (index == 0)
            {
                Disease.Severity = "Nhẹ";
            }
            else if (index == 1)
            {
                Disease.Severity = "Cảnh báo";
            }
            else if (index == 2)
            {
                Disease.Severity = "Nguy hiểm";
            }
        }

        private void SetDiseaseGroup(int index)
        {
            if (index == 0)
            {
                Disease.DiseaseGroup = "Virus";
            }
            else if (index == 1)
            {
                Disease.DiseaseGroup = "Vi Khuẩn";
            }
            else if (index == 2)
            {
                Disease.DiseaseGroup = "Ký Sinh";
            }
        }


        #endregion Prop

        // Thực hiện load toàn bộ du liệu dịch bệnh
        public IAsyncRelayCommand LoadDiseasesCommand { get; }

        private async Task GetAllDiseasesAsync()
        {
            if (_diseaseService is null)
            {
                return;
            }
            var diseases = await _diseaseService.GetAllAsync();
            Diseases = new ObservableCollection<DiseaseType>(diseases);
        }

        // Thực hiện cập nhật dữ liệu dịch bệnh
        public IAsyncRelayCommand UpdateDiseaseCommand { get; }

        private async Task UpdateDiseaseAsync()
        {
       
            if (_diseaseService is null)
            {
                return;
            }
            var question = MessageBox.Show("Bạn muốn cập nhật dữ liệu dịch bệnh ?", "Cập Nhật Dữ Liệu", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Cancel) return;
            SetSeverity(DiseaseSeverityIndex);
            SetDiseaseGroup(DiseaseGroupIndex);
            await _diseaseService.UpdateAsync(Disease);
            await GetAllDiseasesAsync();
        }

        // Thêm dữ liệu dịch bệnh
        public IAsyncRelayCommand AddDiseaseCommand { get; }

        private async Task AddDiseaseAsync()
        {

            if (_diseaseService is null)
            {
                return;
            }
            var question = MessageBox.Show("Bạn muốn thêm dữ liệu dịch bệnh ?", "Thêm Dữ Liệu", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (question == MessageBoxResult.Cancel) return;
            SetSeverity(DiseaseSeverityIndexAdd);
            SetDiseaseGroup(DiseaseGroupIndexAdd);
            await _diseaseService.AddAsync(Disease);
            await GetAllDiseasesAsync();
        }

    }
}
