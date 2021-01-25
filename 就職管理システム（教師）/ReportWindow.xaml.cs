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
    /// ReportWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class ReportWindow : Window
    {

        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //企業情報の登録
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            RecruitTableTableAdapter recruitTable =
            new RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();

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

        public ReportWindow()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            recruitManagement = ((就職管理システム_教師_.RecruitManagementDataBaseDataSet)
                (this.FindResource("recruitManagement")));

            recruitTable.Fill(recruitManagement.RecruitTable);


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
    }
}
