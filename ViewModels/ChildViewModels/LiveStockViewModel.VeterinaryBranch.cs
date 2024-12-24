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
    // Chi cuc thu y
    public partial class LiveStockViewModel
    {
        #region Properties

        //Pagination
        private PaginationHelper<VeterinaryBranch> _Pagination_VeterinaryBranch;
        public PaginationHelper<VeterinaryBranch> Pagination_VeterinaryBranch
        {
            get => _Pagination_VeterinaryBranch;
            set => SetProperty(ref _Pagination_VeterinaryBranch, value);
        }
        // List of Veterinary Branches
        private ObservableCollection<VeterinaryBranch> _VeterinaryBranchList;
        public ObservableCollection<VeterinaryBranch> VeterinaryBranchList
        {
            get { return _VeterinaryBranchList; }
            set
            {
                if (_VeterinaryBranchList != value)
                {
                    _VeterinaryBranchList = value;
                    OnPropertyChanged();
                }
            }
        }

        // Veterinary Branch
        private VeterinaryBranch _veterinaryBranch = new();
        public VeterinaryBranch VeterinaryBranch
        {
            get { return _veterinaryBranch; }
            set
            {
                if (_veterinaryBranch != value)
                {
                    _veterinaryBranch = value;
                    OnPropertyChanged();
                }
            }
        }
        // Veterinary Branch Update
        private VeterinaryBranch _veterinaryBranchUpdate = new();
        public VeterinaryBranch VeterinaryBranchUpdate
        {
            get { return _veterinaryBranchUpdate; }
            set
            {
                if (_veterinaryBranchUpdate != value)
                {
                    _veterinaryBranchUpdate = value;
                    OnPropertyChanged();
                }
            }
        }
        // Selected Veterinary Branch
        private VeterinaryBranch _SelectedVeterinaryBranch;
        public VeterinaryBranch SelectedVeterinaryBranch
        {
            get { return _SelectedVeterinaryBranch; }
            set
            {
                SetProperty(ref _SelectedVeterinaryBranch, value);
                if (value != null)
                {
                    VeterinaryBranchUpdate = value.Clone(); // Create a copy
                }
            }
        }

        // Search
        private string _SearchVeterinaryBranch;
        public string SearchVeterinaryBranch
        {
            get { return _SearchVeterinaryBranch; }
            set
            {
                if (_SearchVeterinaryBranch != value)
                {
                    _SearchVeterinaryBranch = value;
                    OnPropertyChanged();
                }
            }
        }



        #endregion

        #region Command
        public IAsyncRelayCommand LoadVeterinaryBranchCommand { get; }
        public IAsyncRelayCommand AddVeterinaryBranchCommand { get; }
        public IAsyncRelayCommand UpdateVeterinaryBranchCommand { get; }
        public IAsyncRelayCommand DeleteVeterinaryBranchCommand { get; }
        public IAsyncRelayCommand SearchVeterinaryBranchCommand { get; }

        #endregion
    }
}
