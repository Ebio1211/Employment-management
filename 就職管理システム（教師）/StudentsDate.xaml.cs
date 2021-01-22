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
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //生徒情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            StudentTableTableAdapter studentTableTable =
            new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

        //コース情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            CourseTableTableAdapter courseTable =
            new RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter();

        public StudentsDate()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
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

            System.Windows.Data.CollectionViewSource 生徒ViewSource
                = ((System.Windows.Data.CollectionViewSource)(this.FindResource("生徒ViewSource")));

        }

        private void btcbi_Click(object sender, RoutedEventArgs e)
        {
            if (cbCourse.SelectedItem != cbiNotCourse)
            {

                //絞り込み処理
                var data = recruitManagement.StudentTable.Where(
                    d => d.Course.Contains(cbCourse.SelectedItem.ToString())
                    );

                dgStudentsData.DataContext = data;

            }
            else
            {
                dgStudentsData.DataContext = recruitManagement.StudentTable;
            }

        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            dgStudentsData.DataContext = recruitManagement.StudentTable;
            if (tbSearch.Text != "")
            {
                var data = recruitManagement.StudentTable.
                    Where(d => d.StudentName.Contains(tbSearch.Text));
                dgStudentsData.DataContext = data;
            }

            
        }

        private void dgStudentsData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            btWatch.IsEnabled = true;
        }

        private void btWatch_Click(object sender, RoutedEventArgs e)
        {
            StudentDataWindow studentData = new StudentDataWindow();

            //学籍番号、氏名の受け渡し
            DataRowView data = (DataRowView)dgStudentsData.SelectedItems[0];

            //学籍番号、氏名受け渡し
            studentData.tbstunum.Text = data.Row[0].ToString();

            studentData.tbstuna.Text = data.Row[1].ToString();

            //ウィンドウの表示
            studentData.ShowDialog();


        }

    }
}