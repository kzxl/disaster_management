using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.ViewModels.Utils;
using disaster_management.Views.SubWindows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.ViewModels.ChildViewModels
{
    public partial class DiseaseViewModel
    {
        private PaginationHelper<Outbreak> _Pagination_Outbreak;
        public PaginationHelper<Outbreak> Pagination_Outbreak
        {
            get => _Pagination_Outbreak; 
            set => SetProperty(ref _Pagination_Outbreak, value);
        }

        private ObservableCollection<Outbreak>? _outBreakList;
        public ObservableCollection<Outbreak>? OutBreakList
        {
            get => _outBreakList;
            set => SetProperty(ref _outBreakList, value);
        }

        private Outbreak _outbreak = new();
        public Outbreak Outbreak
        {
            get => _outbreak;
            set => SetProperty(ref _outbreak, value);
        }

        private Outbreak _selectedOutbreak;
        public Outbreak SelectedOutbreak
        {
            get => _selectedOutbreak;
            set { 
                SetProperty(ref _selectedOutbreak, value); 
               // if(value !=null)
                   // Outbreak = value.Clone();
            }
        }

        private DiseaseType _diseaseItem;

        public DiseaseType DiseaseItem
        {
            get { return _diseaseItem; }
            set { _diseaseItem = value; 
                OnPropertyChanged();
                if (value != null)
                {
                    UserDiseaseId = value.DiseaseId;
                    Outbreak.DiseaseId = value.DiseaseId;
                    
                }
            }
        }

        private int _UserDiseaseId;

        public int UserDiseaseId
        {
            get { return _UserDiseaseId; }
            set { _UserDiseaseId = value; OnPropertyChanged(); }
        }


        private int _SelectedIndexStatusOutBreak;

        public int SelectedIndexStatusOutBreak
        {
            get { return _SelectedIndexStatusOutBreak; }
            set
            {
                if (_SelectedIndexStatusOutBreak != value)
                {
                    _SelectedIndexStatusOutBreak = value;
                    OnPropertyChanged();
                 
                }
               
            }
        }


        #region CRUD
        public IAsyncRelayCommand LoadOutBreakCommand { get; }
        private async Task GetAllOutBreakAsync()
        {
            if (_outbreakService is null)
            {
                return;
            }
            var outbreak = await _outbreakService.GetAllAsync();
            OutBreakList = new ObservableCollection<Outbreak>(outbreak);

            // Initialization PaginationHelper
            Pagination_Outbreak = new PaginationHelper<Outbreak>(OutBreakList, 18);
        }

        public IAsyncRelayCommand AddOutbreakCommand { get; }
        private async Task AddOutBreakAsync()
        {
            if (_SelectedIndexStatusOutBreak == 0)
            {
                Outbreak.Status = "Mới phát hiện";
            }
            else if (SelectedIndexStatusOutBreak == 1)
            {
                Outbreak.Status = "Đang xử lý";
            }
            else if (_SelectedIndexStatusOutBreak == 2)
            {
                Outbreak.Status = "Đã xử lý";
            }
            await _outbreakService.AddAsync(Outbreak);
            await GetAllOutBreakAsync(); // Reload
        }


        public IAsyncRelayCommand DeleteOutbreakCommand { get; }
        private async Task DeleteOutbreakAsync()
        {
            await _outbreakService.DeleteAsync(SelectedOutbreak.OutbreakId);
            await GetAllOutBreakAsync(); // Reload
        }
        public IAsyncRelayCommand UpdateOutbreakCommand { get; }

        private async Task UpdateOutbreakAsync()
        {
            await _outbreakService.UpdateAsync()
        }

        public IAsyncRelayCommand SelectDiseaseIDCommand { get; }
        //private async Task SelectDiseaseIDAsync()
        //{
        //    if (SelectedOutbreak is null)
        //    {
        //        return;
        //    }
        //    Outbreak = SelectedOutbreak;

        //}
        #endregion

        public IRelayCommand OpenPopupSelectDiseaseCommand { get; }

        private void OpenPopupSelectDisease()
        {
            var popupWindow = new SelectDisease
            {
                DataContext = this // Sử dụng chung ViewModel
            };

            popupWindow.ShowDialog();
        }

    }
}
