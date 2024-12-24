﻿using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.ViewModels.Utils;
using System.Collections.ObjectModel;

namespace disaster_management.ViewModels.ChildViewModels
{
    // lo mo
    public partial class LiveStockViewModel
    {
        //Pagination
        private PaginationHelper<Slaughterhouse> _Pagination_Slaughterhouse;
        public PaginationHelper<Slaughterhouse> Pagination_Slaughterhouse
        {
            get => _Pagination_Slaughterhouse;
            set => SetProperty(ref _Pagination_Slaughterhouse, value);
        }
        // List
        private ObservableCollection<Slaughterhouse> _SlaughterhouseList;
        public ObservableCollection<Slaughterhouse> SlaughterhouseList
        {
            get { return _SlaughterhouseList; }
            set
            {
                if (_SlaughterhouseList != value)
                {
                    _SlaughterhouseList = value;
                    OnPropertyChanged();
                }
            }
        }
        // Slaughterhouse
        private Slaughterhouse _slaughterhouse = new();
        public Slaughterhouse Slaughterhouse
        {
            get { return _slaughterhouse; }
            set
            {
                if (_slaughterhouse != value)
                {
                    _slaughterhouse = value;
                    OnPropertyChanged();
                }
            }
        }
        // Slaughterhouse Update
        private Slaughterhouse _slaughterhouseUpdate = new();
        public Slaughterhouse SlaughterhouseUpdate
        {
            get { return _slaughterhouseUpdate; }
            set
            {
                if (_slaughterhouseUpdate != value)
                {
                    _slaughterhouseUpdate = value;
                    OnPropertyChanged();
                }
            }
        }
        // Selected Slaughterhouse
        private Slaughterhouse _selectedSlaughterhouse;
        public Slaughterhouse SelectedSlaughterhouse
        {
            get { return _selectedSlaughterhouse; }
            set
            {
                if (_selectedSlaughterhouse != value)
                {
                    _selectedSlaughterhouse = value;
                    OnPropertyChanged();
                }
            }
        }
        // search
        private string _searchSlaughterhouse;
        public string SearchSlaughterhouse
        {
            get { return _searchSlaughterhouse; }
            set
            {
                SetProperty(ref _searchSlaughterhouse, value);
               
            }
        }


        #region Command
        public IAsyncRelayCommand LoadSlaughterhouseCommand { get; }
        public IAsyncRelayCommand AddSlaughterhouseCommand { get; }
        public IAsyncRelayCommand UpdateSlaughterhouseCommand { get; }
        public IAsyncRelayCommand DeleteSlaughterhouseCommand { get; }
        public IAsyncRelayCommand SearchSlaughterhouseCommand { get; }

        #endregion
    }
}
