using disaster_management.Views.SubWindows;
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
    /// Interaction logic for DiseaseManagement.xaml
    /// </summary>
    public partial class DiseaseManagement : UserControl
    {
        public DiseaseManagement()
        {
            InitializeComponent();
        }

        private void Button_ChonLoaiDichBenh(object sender, RoutedEventArgs e)
        {
           SelectDisease selectDisease = new SelectDisease();
            selectDisease.ShowDialog();
        }

        private void Button_ChonODichBenh(object sender, RoutedEventArgs e)
        {
            SelectOutbreak selectOutbreak = new SelectOutbreak();
            selectOutbreak.ShowDialog();
        }
    }
}
