using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.ViewModels.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.ViewModels.ChildViewModels
{
    public partial class DiseaseViewModel : ObservableObject
    {

        #region Properties

        private PaginationHelper<OutbreakDiagnosis> _Pagination_OD;
        public PaginationHelper<OutbreakDiagnosis> Pagination_OD
        {
            get { return _Pagination_OD; }
            set
            {
                if (_Pagination_OD != value)
                {
                    _Pagination_OD = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<OutbreakDiagnosis>? _od;
        public ObservableCollection<OutbreakDiagnosis>? OD
        {
            get => _od;
            set => SetProperty(ref _od, value);
        }

        private OutbreakDiagnosis _outbreakDia;
        public OutbreakDiagnosis OutbreakDia
        {
            get => _outbreakDia;
            set => SetProperty(ref _outbreakDia, value);
        }

        private OutbreakDiagnosis _SelectedOutbreakDia;
        public OutbreakDiagnosis SelectedOutbreakDia
        {
            get { return _SelectedOutbreakDia; }
            set
            {
                SetProperty(ref _SelectedOutbreakDia, value);
                if (value != null)
                {
                    OutbreakDia = value.Clone(); // Create a copy
                }
            }
        }

        #endregion


        public IAsyncRelayCommand LoadODCommand { get; }
        private async Task GetAllODAsync()
        {
            if (_outbreakDiagnosisService is null)
            {
                return;
            }
            var ods = await _outbreakDiagnosisService.GetAllAsync();
            OD = new ObservableCollection<OutbreakDiagnosis>(ods);

            // Initialization PaginationHelper
            Pagination_OD = new PaginationHelper<OutbreakDiagnosis>(OD, 18);
        }


    }
}
