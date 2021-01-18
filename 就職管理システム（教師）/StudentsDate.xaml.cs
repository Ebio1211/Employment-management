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
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
                StudentTableTableAdapter studentTableTable =
                new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

            studentTableTable.Fill(recruitManagement.StudentTable);

            System.Windows.Data.CollectionViewSource 生徒ViewSource
                = ((System.Windows.Data.CollectionViewSource)(this.FindResource("生徒ViewSource")));


        }
    }
}
