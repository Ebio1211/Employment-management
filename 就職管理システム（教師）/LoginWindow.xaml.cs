using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// LoginWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginWindow : Window
    {
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;
        BindingList<RecruitManagementDataBaseDataSet> recruits =
            new BindingList<RecruitManagementDataBaseDataSet>();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
                TeachersTableTableAdapter teachers =
                new RecruitManagementDataBaseDataSetTableAdapters.TeachersTableTableAdapter();
            teachers.Fill(recruitManagement.TeachersTable);

            string join = MALogin.Text + tblAddress.Text;
            var data = recruitManagement.TeachersTable.Where(d => d.TeacherMail.Contains(join)).ToList();

            if (data.Exists(s=>s.TeacherMail.Contains(join)))
            {
                MALogin.Text = "";
                MainWindow mainWindow = new MainWindow();
                mainWindow.ShowDialog();
            }else
            {
                MessageBoxResult result = MessageBox.Show("メールアドレスが間違っています"
                    , "警告", MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    MALogin.Text = "";
                    btLogin.IsEnabled = false;
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void MALogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MALogin.Text))
            {
                btLogin.IsEnabled = true;
            }
            else
            {
                btLogin.IsEnabled = false;
            }
            
        }
    }
}
