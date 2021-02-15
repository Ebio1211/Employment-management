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

        System.Windows.Data.CollectionViewSource 企業ViewSource;


        //教師ID
        public string teachername { get; set; }

        //学籍番号
        public string stunumber { get; set; }
        //氏名
        public string stname { get; set; }

        //ID
        public string recId { get; set; }
        //企業名
        public string companyget { get; set; }
        //活動場所
        public string pressget { get; set; }
        //種別
        public string typege { get; set; }
        //活動日
        public string date { get; set; }
        //評価
        public string evalu { get; set; }


        public StudentDataWindow()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            StudentsDate students = new StudentsDate();
            students.teachername = this.teachername;

            students.Show();
            this.Close();
        }

        private void btWatch_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow report = new ReportWindow();

            //企業名、活動場所、種別の取得
            var data = (DataRow)dgStudentData.SelectedItem;

            report.teachername = this.teachername;

            //企業名、活動場所、種別受け渡し

            report.companyget = data[1].ToString();

            report.pressget = data[2].ToString();

            report.typege = data[3].ToString();

            //データの格納
            //企業No
            report.recId = data[0].ToString();
            //生徒名
            report.name = tbstuna.Text;
            //学籍番号
            report.number = data[5].ToString();
            //活動日
            report.date = data[4].ToString();
            //評価
            report.evalu = data[10].ToString();

            report.Show();

            this.Close();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            recruitTable.Fill(recruitManagement.RecruitTable);

            cbCorporation.SelectedItem = cbiNotCorporation;
            cbPrefectures.SelectedItem = cbiNotPrefectures;

            tbstunum.Text = stunumber;
            tbstuna.Text = stname;

            


            //企業名の情報を登録



            var demplo = recruitManagement.
                RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(stunumber)
                ).Select(s => s.EmployeeName).Distinct().ToList();
            foreach (var emitem in demplo)
            {
                cbCorporation.Items.Add(emitem.ToString());
            }

            var dprefe = recruitManagement.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(stunumber)
                ).Select(d => d.Place).Distinct().ToList();
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

            if (!string.IsNullOrWhiteSpace(tbstunum.Text))
            {
                //学籍番号で絞り込む
                
                dgStudentData.DataContext = recruitManagement.
                    RecruitTable.AsEnumerable().Where(
                    d => d.StudenNumber.ToString().Contains(
                        tbstunum.Text.ToString()
                        )
                    ).Select(s=>s).ToArray();

            }
            dgStudentData.SelectedIndex = -1;
            btWatch.IsEnabled = false;

        }


        private void dgStudentData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgStudentData.SelectedIndex > -1)
            {
                var data = (DataRow)dgStudentData.SelectedItem;

                string evdata = data[10].ToString();

                if (evdata == "未提出" || evdata == "再提出")
                {
                    btWatch.IsEnabled = false;
                }
                else
                {
                    btWatch.IsEnabled = true;
                }

            }
            
        }

        //絞り込み
        private void btcbiitem_Click(object sender, RoutedEventArgs e)
        {
            if (cbCorporation.SelectedItem != cbiNotCorporation && cbPrefectures.SelectedItem != cbiNotPrefectures)
            {
                EmployPrefec();
            }
            else if (cbCorporation.SelectedItem != cbiNotCorporation && cbPrefectures.SelectedItem == cbiNotPrefectures)
            {
                Employnaitem();
            }
            else if (cbCorporation.SelectedItem == cbiNotCorporation && cbPrefectures.SelectedItem != cbiNotPrefectures)
            {
                Prefecturesitem();
            }
            else
            {
                dgStudentData.DataContext = recruitManagement.RecruitTable.AsEnumerable().Select(s => s).ToArray();
            }
            dgStudentData.SelectedIndex = -1;
        }

        //絞り込み処理  企業名
        public void Employnaitem()
        {
            
            var data = recruitManagement.RecruitTable.Where(
                d => d.EmployeeName.Contains(cbCorporation.SelectedItem.ToString())
                );

            dgStudentData.DataContext = data;
        }

        //絞り込み処理  活動場所
        public void Prefecturesitem()
        {

            var data = recruitManagement.RecruitTable.Where(
                d => d.Place.Contains(cbPrefectures.SelectedItem.ToString())
                );

            dgStudentData.DataContext = data;
        }

        //絞り込み処理  両方
        public void EmployPrefec()
        {

            var data = recruitManagement.RecruitTable.Where(
                d => d.Place.Contains(cbPrefectures.SelectedItem.ToString())&&
                d.EmployeeName.Contains(cbCorporation.SelectedItem.ToString())
                );

            dgStudentData.DataContext = data;
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbSearch.Text != "")
            {
                var data = recruitManagement.RecruitTable.
                    Where(d => d.EmployeeName.Contains(tbSearch.Text));
                dgStudentData.DataContext = data;
            }
        }
    }
}
