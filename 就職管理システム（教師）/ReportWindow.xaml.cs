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
using 就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters;

namespace 就職管理システム_教師_
{
    /// <summary>
    /// ReportWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ReportWindow : Window
    {

        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //企業情報の登録
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            RecruitTableTableAdapter recruitTable =
            new RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();

        //評価の登録
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            EvaluationTableTableAdapter evaluation =
            new RecruitManagementDataBaseDataSetTableAdapters.EvaluationTableTableAdapter();

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
        //学籍番号
        public string number { get; set; }
        //氏名
        public string name { get; set; }
        //評価
        public string evalu { get; set; }

        //教師ID
        public string teachername { get; set; }

        bool editmemo = false;


        public ReportWindow()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            StudentDataWindow recruit = new StudentDataWindow();

            recruit.teachername = this.teachername;
            recruit.stunumber = this.number;
            recruit.stname = this.name;
            recruit.Show();

            this.Close();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            recruitTable.Fill(recruitManagement.RecruitTable);
            evaluation.Fill(recruitManagement.EvaluationTable);

            tbCompany.Text = companyget;
            tbPress.Text = pressget;
            tbType.Text = typege;


            //当日の内容にデータベースの内容を表示

            var daycontent = recruitManagement.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(number
                    ) && d.RecruitDate.ToString().Contains(date)
                ).Select(b=>b.dateContent.ToString()).ToArray();

            tbdayReport.Text = daycontent[0];

            //その他にデータベースの内容を表示

            var another = recruitManagement.RecruitTable.Where(
                d => d.StudenNumber.ToString().Contains(number
                    ) && d.RecruitDate.ToString().Contains(date)
                ).Select(b => b.Others.ToString()).ToArray();

            if (!string.IsNullOrWhiteSpace(another[0]))
            {
                tbAnother.Text = another[0];
            }


        }

        private void btResubmit_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                evalu = "再提出";
                recruitTable.Fill(recruitManagement.RecruitTable);

                //該当行を抽出
                var report = recruitManagement.RecruitTable.Where(
                    d => d.RecruitID.ToString().Contains(recId)).ToList();

                //評価を格納する
                report[0].Evaluation = evalu;

                //教師メモが編集されてたら登録
                if (editmemo)
                {
                    report[0].TeachersMemo = tbTeacher.Text;
                }


                //データベース更新
                recruitTable.Adapter.Update(recruitManagement.RecruitTable);

                MessageBoxResult result = MessageBox.Show("レポートを評価しました。"
                   , "警告", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show("登録に失敗しました。" + "\n" + ex.Message);
            }
        }

        private void tbTeacher_TextChanged(object sender, TextChangedEventArgs e)
        {
            editmemo = true;
        }

        private void btCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                evalu = "確認済み";
                recruitTable.Fill(recruitManagement.RecruitTable);

                //該当行を抽出
                var report = recruitManagement.RecruitTable.Where(
                    d => d.RecruitID.ToString().Contains(recId)).ToList();

                //評価を格納する
                report[0].Evaluation = evalu;

                //教師メモが編集されてたら登録
                if (editmemo)
                {
                    report[0].TeachersMemo = tbTeacher.Text;
                }


                //データベース更新
                recruitTable.Adapter.Update(recruitManagement.RecruitTable);

                MessageBoxResult result = MessageBox.Show("レポートを評価しました。"
                   , "警告", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                MessageBox.Show("登録に失敗しました。" + "\n" + ex.Message);
            }
        }
    }
}
