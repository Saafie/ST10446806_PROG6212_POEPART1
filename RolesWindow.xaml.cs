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
using System.Windows.Shapes;

namespace ST10446806_PROG6212_POEPART1
{

    public partial class RolesWindow : Window
    {
        public RolesWindow()
        {
            InitializeComponent();
          
        }

   
        private void ClickLecturer(object sender, RoutedEventArgs l)
        {
            LecturerWindow lecturerWindow = new LecturerWindow();
            lecturerWindow.Show();
        }

        private void ClickCoordinator(object sender, RoutedEventArgs c)
        {
            CoordinatorWindow coordinatorWindow = new CoordinatorWindow();
            coordinatorWindow.Show();
        }

        private void ClickManager(object sender, RoutedEventArgs c)
        {
            ManagerWindow managerWindow = new ManagerWindow();
            managerWindow.Show();
        }
    }
}


