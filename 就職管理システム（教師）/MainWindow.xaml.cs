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

namespace 就職管理システム_教師_
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btRegistration_Click(object sender, RoutedEventArgs e)
        {
            InformationRegistrationWindow information = new InformationRegistrationWindow();
            information.ShowDialog();
        }

        private void btBrowsing_Click(object sender, RoutedEventArgs e)
        {
            StudentsDate students = new StudentsDate();
            students.ShowDialog();
        }


    }
}
