using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        System.Windows.Data.CollectionViewSource 生徒ViewSource;
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //生徒情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            StudentTableTableAdapter studentTableTable =
            new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

        //コース情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            CourseTableTableAdapter courseTable =
            new RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter();

        //教師ID
        public string teachername { get; set; }

        public StudentsDate()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            main.teachername = this.teachername;
            main.Show();

            this.Close();
        }


        protected void Window_Loaded(object sender, RoutedEventArgs e)
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
            生徒ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("生徒ViewSource")));

            dgStudentsData.SelectedIndex = -1;

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

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                var data = recruitManagement.StudentTable.
                    Where(d => d.StudentName.Contains(tbSearch.Text));
                dgStudentsData.DataContext = data;
            }

            
        }

        private void dgStudentsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgStudentsData.SelectedIndex > -1)
            {
                btWatch.IsEnabled = true;
            }
            else
            {
                btWatch.IsEnabled = false;
            }
            
        }

        private void btWatch_Click(object sender, RoutedEventArgs e)
        {
            StudentDataWindow studentData = new StudentDataWindow();

            studentData.teachername = this.teachername;

            //学籍番号、氏名の受け渡し

            var data = (DataRow)dgStudentsData.SelectedItem;
            
            //学籍番号、氏名受け渡し
            studentData.stunumber = data[0].ToString();
            studentData.stname = data[1].ToString();

            //ウィンドウの表示
            studentData.Show();

            this.Close();


        }

    }
}