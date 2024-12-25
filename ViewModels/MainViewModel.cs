﻿using CommunityToolkit.Mvvm.ComponentModel;
using disaster_management.ViewModels.ChildViewModels;
using disaster_management.ViewModels.ChildViewModels.Disaster;
using disaster_management.ViewModels.ChildViewModels.Users;

namespace disaster_management.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public DisasterViewModel DisasterViewModel { get; }
        public DiseaseViewModel DiseaseViewModel { get; }
        public LiveStockViewModel LiveStockViewModel { get; }
        public UserViewModel UserViewModel { get; }


        public MainViewModel(DiseaseViewModel diseaseViewModel, 
            LiveStockViewModel liveStockViewModel,
            DisasterViewModel disasterViewModel,
            UserViewModel userViewModel)
        {
            DiseaseViewModel = diseaseViewModel;
            LiveStockViewModel = liveStockViewModel;
            DisasterViewModel = disasterViewModel;
            UserViewModel = userViewModel;
        }

    }
}
