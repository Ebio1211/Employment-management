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
using 就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters;

namespace 就職管理システム_教師_
{
    /// <summary>
    /// InformationRegistrationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InformationRegistrationWindow : Window
    {
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //生徒情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            StudentTableTableAdapter studentTableTable =
            new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

        //コース情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            CourseTableTableAdapter courseTable =
            new RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter();

        public InformationRegistrationWindow()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            Cancel_Judge();
        }

        //入力途中の文字があるか？
        public void Cancel_Judge()
        {
            if (!string.IsNullOrWhiteSpace(tbgakuseki.Text) || !string.IsNullOrWhiteSpace(tbClass.Text)
                || !string.IsNullOrWhiteSpace(tbName.Text) || !string.IsNullOrWhiteSpace(tbMaill.Text))
            {
                MessageBoxResult result = MessageBox.Show("本当に閉じますか？", "警告", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRegistration registration = new DataRegistration();
            registration.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            //コースの情報を登録
            courseTable.Fill(recruitManagement.CourseTable);

            var datacm = recruitManagement.CourseTable.Select(s => s.Course).ToList();
            foreach (var cmitem in datacm)
            {
                cbCourse.Items.Add(cmitem.ToString());
            }
        }

        private void btTouroku_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbgakuseki.Text)||
                string.IsNullOrWhiteSpace(tbName.Text)||
                string.IsNullOrWhiteSpace(tbClass.Text)||
                string.IsNullOrWhiteSpace(tbMaill.Text)
                )
            {
                MessageBoxResult result = MessageBox.Show("入力漏れがあります。"
                    , "警告", MessageBoxButton.OK);
            }
            else
            {
                //新規レコードの追加
                var Newdr = recruitManagement.StudentTable.NewRow();

                Newdr[0] = tbgakuseki.Text;
                Newdr[1] = tbName.Text;
                Newdr[2] = cbCourse.SelectedItem;
                Newdr[3] = tbClass.Text;
                Newdr[4] = tbMaill.Text + tbAddress.Text;

                //データセットに新しいレコードを追加
                recruitManagement.StudentTable.Rows.Add(Newdr);

                //データベース更新
                studentTableTable.Adapter.Update(recruitManagement.StudentTable);
            }
        }
    }
}
