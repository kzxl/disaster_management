﻿using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.ViewModels.Utils;
using System.Collections.ObjectModel;

namespace disaster_management.ViewModels.ChildViewModels
{
    // Vung tam giu
    public partial class LiveStockViewModel
    {
     
        //Pagination
        private PaginationHelper<TemporaryZone> _Pagination_TemporaryZone;
        public PaginationHelper<TemporaryZone> Pagination_TemporaryZone
        {
            get
            {
                if (_Pagination_TemporaryZone == null)
                {
                    _Pagination_TemporaryZone = new PaginationHelper<TemporaryZone>(this.TemporaryZones);
                }
                return _Pagination_TemporaryZone;
            }
        }
        // List
        private ObservableCollection<TemporaryZone> _TemporaryZones;
        public ObservableCollection<TemporaryZone> TemporaryZones
        {
            get { return _TemporaryZones; }
            set
            {
                if (_TemporaryZones != value)
                {
                    _TemporaryZones = value;
                    OnPropertyChanged();
                }
            }
        }
        // Temporary Zone
        private TemporaryZone _temporaryZone = new();
        public TemporaryZone TemporaryZone
        {
            get { return _temporaryZone; }
            set
            {
                if (_temporaryZone != value)
                {
                    _temporaryZone = value;
                    OnPropertyChanged();
                }
            }
        }
        // Temporary Zone Update
        private TemporaryZone _temporaryZoneUpdate = new();
        public TemporaryZone TemporaryZoneUpdate
        {
            get { return _temporaryZoneUpdate; }
            set
            {
                if (_temporaryZoneUpdate != value)
                {
                    _temporaryZoneUpdate = value;
                    OnPropertyChanged();
                }
            }
        }
        // Selected Temporary Zone
        private TemporaryZone _SelectedTemporaryZone;
        public TemporaryZone SelectedTemporaryZone
        {
            get { return _SelectedTemporaryZone; }
            set
            {
                SetProperty(ref _SelectedTemporaryZone, value);
               
            }
        }
        // Search Temporary Zone
        private string _SearchTemporaryZone;
        public string SearchTemporaryZone
        {
            get { return _SearchTemporaryZone; }
            set
            {
                if (_SearchTemporaryZone != value)
                {
                    _SearchTemporaryZone = value;
                    OnPropertyChanged();
                }
            }
        }

        #region Command
        public IAsyncRelayCommand LoadTemporaryZoneCommand { get; }
        public IAsyncRelayCommand AddTemporaryZoneCommand { get; }
        public IAsyncRelayCommand UpdateTemporaryZoneCommand { get; }
        public IAsyncRelayCommand DeleteTemporaryZoneCommand { get; }
        public IAsyncRelayCommand SearchTemporaryZoneCommand { get; }

        #endregion

    }
}
