using System;
using System.Collections.Generic;
using System.Data;
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
    /// DataRegistration.xaml の相互作用ロジック
    /// </summary>
    public partial class DataRegistration : Window
    {
        public string teachername { get; set; }
        System.Windows.Data.CollectionViewSource UpdateViewSource;
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //生徒情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            StudentTableTableAdapter studentTableTable =
            new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

        //コース情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            CourseTableTableAdapter courseTable =
            new RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter();
        public DataRegistration()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            InformationRegistrationWindow information = new InformationRegistrationWindow();
            information.Show();
            this.Close();
        }

        private void btcbi_Click(object sender, RoutedEventArgs e)
        {
            if (cbCourse.SelectedItem != cbiNotCourse)
            {

                //絞り込み処理
                var data = recruitManagement.StudentTable.AsEnumerable().Where(
                    d => d.Course.Contains(cbCourse.SelectedItem.ToString())
                    );

                dgStudentsData.DataContext = data;

            }
            else
            {
                dgStudentsData.DataContext = recruitManagement.StudentTable.AsEnumerable().Select(s => s).ToArray();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
            (this.FindResource("recruitManagement")));

            cbCourse.SelectedItem = cbiNotCourse;
            //コースの情報を登録
            courseTable.Fill(recruitManagement.CourseTable);

            var datacm = recruitManagement.CourseTable.Select(s => s.Course).ToList();
            foreach (var cmitem in datacm)
            {
                cbCourse.Items.Add(cmitem.ToString());
            }

            studentTableTable.Fill(recruitManagement.StudentTable);

            //データコンテキストにリンク使用(超重大)
            dgStudentsData.DataContext =
                recruitManagement.StudentTable.AsEnumerable().Select(s => s).ToArray();

            UpdateViewSource = ((System.Windows.Data.CollectionViewSource)
                (this.FindResource("UpdateViewSource")));
        }

        private void btWatch_Click(object sender, RoutedEventArgs e)
        {
            InformationRegistrationWindow information = new InformationRegistrationWindow();

            //データを取得
            var data = (DataRow)dgStudentsData.SelectedItem;

            //データを格納

            information.stunumber = data[0].ToString();
            information.stname = data[1].ToString();
            information.course = data[2].ToString();
            information.clas = data[3].ToString();
            information.maill = data[4].ToString();

            information.teachername = this.teachername;

            information.Show();

            this.Close();
        }

        //削除
        private void btDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("データを削除しますか？"
                  , "警告", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int remdata = dgStudentsData.SelectedIndex;

                    //recruitManagement.StudentTable.Rows.Remove(
                    //    recruitManagement.StudentTable.Rows[remdata]);

                    var datar = (DataRow)recruitManagement.StudentTable.Rows[remdata];

                    datar.Delete();

                    //データベース更新
                    studentTableTable.Adapter.Update(recruitManagement.StudentTable);

                    Window_Loaded(sender, e);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("更新に失敗しました。" + "\n" + ex.Message);
            }
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                var data = recruitManagement.StudentTable.
                    Where(d => d.StudentName.Contains(tbSearch.Text));
                dgStudentsData.DataContext = data;
            }
        }
    }
}
