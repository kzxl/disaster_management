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

        private Outbreak _outbreak;
        public Outbreak Outbreak
        {
            get => _outbreak;
            set => SetProperty(ref _outbreak, value);
        }

        private Outbreak _selectedOutbreak;
        public Outbreak SelectedOutbreak
        {
            get => _selectedOutbreak;
            set { SetProperty(ref _selectedOutbreak, value); Outbreak = value; }
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
        #endregion

    }
}
