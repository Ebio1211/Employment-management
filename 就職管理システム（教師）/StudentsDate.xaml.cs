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

namespace 就職管理システム_教師_
{
    /// <summary>
    /// StudentsDate.xaml の相互作用ロジック
    /// </summary>
    public partial class StudentsDate : Window
    {
        public StudentsDate()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btWatch_Click(object sender, RoutedEventArgs e)
        {
            StudentDataWindow studentData = new StudentDataWindow();
            studentData.ShowDialog();
        }
    }
}
