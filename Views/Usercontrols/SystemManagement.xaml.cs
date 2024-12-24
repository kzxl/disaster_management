using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace disaster_management.Views.Usercontrols
{
    /// <summary>
    /// Interaction logic for SystemManagement.xaml
    /// </summary>
    public partial class SystemManagement : UserControl
    {
        public SystemManagement()
        {
            InitializeComponent();
        }
        private void ComboBoxAdminstrativeSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboboxAdministrative.SelectedItem is ComboBoxItem selectedItem)
            {
                string level = selectedItem.Content.ToString();
                lbAdminstartiveName.Text = $"Tên {level}:";
                lbAdminstartiveName_Edit.Text = $"Tên {level}:";
                // Nếu chọn "Tỉnh", ẩn ComboBoxParent
                if (level == "Tỉnh")
                {
                    ParentSelectionPanel.Visibility = Visibility.Collapsed;
                    ParentSelectionPanel_Edit.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ParentSelectionPanel.Visibility = Visibility.Visible;
                    ParentSelectionPanel_Edit.Visibility=Visibility.Visible;
                    // Hiển thị danh sách dựa trên cấp độ
                   //load dữ liệu
                }
            }
        }
        // Hàm lấy danh sách cấp trên
       
    }
}
}
