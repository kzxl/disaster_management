using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using disaster_management.Models;
using disaster_management.Services;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.ViewModels.ChildViewModels.Users
{
   public partial class UserViewModel : ObservableObject
    {
        private readonly IUserService userService;
        private readonly IUserGroupService userGroupService;
        private readonly IUserRoleService userRoleService;
        private readonly IUserLogService userLogService;
        public UserViewModel(IUserService userService,IUserGroupService userGroupService, IUserRoleService userRoleService, IUserLogService userLogService)
        {
            this.userService = userService;
            this.userGroupService = userGroupService;
            this.userRoleService = userRoleService;
            this.userLogService = userLogService;
            LoadCurrentUserCommand = new AsyncRelayCommand(LoadCurrentUserAsync);
            LoadListAsync();

        }

        private async void LoadListAsync()
        {
            await LoadCurrentUserCommand.ExecuteAsync(null);
        }

        private async Task LoadCurrentUserAsync()
        {
            CurrentUser = await userService.ValidateUser("admin", "hash_password_789");
        }

        public IAsyncRelayCommand LoadCurrentUserCommand;

        private User _user = new();
        public User CurrentUser
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); UserRole = CurrentUser.Groups.FirstOrDefault().Roles.FirstOrDefault(); }
        }

        private UserRole _ur;

        public UserRole UserRole
        {
            get { return _ur; }
            set { _ur = value; OnPropertyChanged(); }
        }

    }
}
