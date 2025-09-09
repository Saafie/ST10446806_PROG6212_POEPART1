using System.Windows;

namespace ST10446806_PROG6212_POEPART1
{
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
