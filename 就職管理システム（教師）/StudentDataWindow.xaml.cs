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
    /// StudentDataWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class StudentDataWindow : Window
    {
        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //企業情報の登録
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            RecruitTableTableAdapter recruitTable =
            new RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();

        //学籍番号
        public string stunumber { get; set; }
        //氏名
        public string stname { get; set; }

        public StudentDataWindow()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btWatch_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow report = new ReportWindow();

            //企業名、活動場所、種別の取得
            DataRowView data = (DataRowView)dgStudentData.SelectedItems[0];

            //企業名、活動場所、種別受け渡し
            report.tbCompany.Text = data.Row[1].ToString();

            report.tbPress.Text = data.Row[2].ToString();

            report.tbType.Text = data.Row[3].ToString();

            //データの格納
            report.number = data.Row[5].ToString();
            report.date = data.Row[4].ToString();

            report.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            recruitTable.Fill(recruitManagement.RecruitTable);

            cbCorporation.SelectedItem = cbiNotCorporation;
            cbPrefectures.SelectedItem = cbiNotPrefectures;

            //企業名の情報を登録

            var demplo = recruitManagement.RecruitTable.Select(s => s.EmployeeName).Distinct().ToList();
            foreach (var emitem in demplo)
            {
                cbCorporation.Items.Add(emitem.ToString());
            }

            var dprefe = recruitManagement.RecruitTable.Select(d => d.Place).Distinct().ToList();
            foreach (var preitem in dprefe)
            {
                cbPrefectures.Items.Add(preitem.ToString());
            }

            System.Windows.Data.CollectionViewSource 企業ViewSource
                = ((System.Windows.Data.CollectionViewSource)(this.FindResource("企業ViewSource")));

            

            //生徒情報を取得
            就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
                StudentTableTableAdapter studentTableTable =
                new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

            studentTableTable.Fill(recruitManagement.StudentTable);

            if (string.IsNullOrWhiteSpace(tbstunum.Text))
            {
                //学籍番号で絞り込む
                var data = recruitManagement.RecruitTable.Where(
                    d => d.StudenNumber.ToString().Contains(
                        tbstunum.Text.ToString()
                        )
                    );
                dgStudentData.DataContext = data;
            }


        }

        private void dgStudentData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView data = (DataRowView)dgStudentData.SelectedItems[0];

            if (data.Row[10].ToString() != "未提出")
            {
                btWatch.IsEnabled = true;
            }        }

        private void btcbiitem_Click(object sender, RoutedEventArgs e)
        {
            if (cbCorporation.SelectedItem != cbiNotCorporation)
            {
                //絞り込み処理
                var data = recruitManagement.RecruitTable.Where(
                    d => d.EmployeeName.Contains(cbCorporation.SelectedItem.ToString())
                    );

                dgStudentData.DataContext = data;
            }
            else
            {
                dgStudentData.DataContext = recruitManagement.RecruitTable;
            }
        }
    }
}
