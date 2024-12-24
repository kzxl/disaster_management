using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.ViewModels.ChildViewModels
{
    public partial class LiveStockViewModel : ObservableObject
    {
        // Service declaration
        private readonly ICertificateService certiticateService;
        private readonly ILivestockFarmService livestockFarmService;
        private readonly ILivestockFarmConditionService livestockFarmConditionService;
        private readonly ISlaughterhouseService slaughterhouse;
        private readonly ISafeLivestockZoneService safeLivestockZoneService;
        private readonly ILivestockStatisticService livestockStatisticService;
        private readonly ITemporaryZoneService temporaryZoneService;
        private readonly IVeterinaryBranchService veterinaryBranchService;
        private readonly IVetMedicineAgencyService vetMedicineAgencyService;    

        public LiveStockViewModel(
            ICertificateService certiticateService,
            ILivestockFarmService livestockFarmService,
            ILivestockFarmConditionService livestockFarmConditionService,
            ISlaughterhouseService slaughterhouse,
            ISafeLivestockZoneService safeLivestockZoneService,
            ILivestockStatisticService livestockStatisticService, 
            ITemporaryZoneService temporaryZoneService,
            IVeterinaryBranchService veterinaryBranchService,
            IVetMedicineAgencyService vetMedicineAgencyService)
        {
            this.certiticateService = certiticateService;
            this.livestockFarmService = livestockFarmService;
            this.livestockFarmConditionService = livestockFarmConditionService;
            this.slaughterhouse = slaughterhouse;
            this.safeLivestockZoneService = safeLivestockZoneService;
            this.livestockStatisticService = livestockStatisticService;
            this.temporaryZoneService = temporaryZoneService;
            this.veterinaryBranchService = veterinaryBranchService;
            this.vetMedicineAgencyService = vetMedicineAgencyService;

            // Command Register
            // Farm
            LoadFarmCommand = new AsyncRelayCommand(LoadFarmAsync);
          //  AddFarmCommand = new AsyncRelayCommand(AddFarmAsync);
         //   UpdateFarmCommand = new AsyncRelayCommand(UpdateFarmAsync);
         //   DeleteFarmCommand = new AsyncRelayCommand(DeleteFarmAsync);


            // Certificate Command
            LoadCertificateCommand = new AsyncRelayCommand(LoadCertificateAsync);
            OpenFarmWindowCommand = new  RelayCommand(OpenPopupSelectFarmID);
            AddCertificateCommand = new AsyncRelayCommand(AddCertificateAsync);
            UpdateCertificateCommand = new AsyncRelayCommand(UpdateCertificateAsync);
            DeleteCertificateCommand = new AsyncRelayCommand(DeleteCertificateAsync);
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            await LoadCertificateCommand.ExecuteAsync(null);
            await LoadFarmCommand.ExecuteAsync(null);
        }


        private LivestockFarm _FarmItem;
        public LivestockFarm FarmItem
        {
            get { return _FarmItem; }
            set { _FarmItem = value; 
                OnPropertyChanged(); 
                Certificate.FarmId = FarmItem.FarmId;
                OnPropertyChanged("Certificate");

                CertificateUpdate.FarmId = FarmItem.FarmId;
                OnPropertyChanged("CertificateUpdate");
            }
        }

    }
}
