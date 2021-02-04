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
        public string teachername { get; set; }

        就職管理システム_教師_.RecruitManagementDataBaseDataSet recruitManagement;

        //生徒情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            StudentTableTableAdapter studentTableTable =
            new RecruitManagementDataBaseDataSetTableAdapters.StudentTableTableAdapter();

        //コース情報を取得
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            CourseTableTableAdapter courseTable =
            new RecruitManagementDataBaseDataSetTableAdapters.CourseTableTableAdapter();

        //学籍番号
        public string stunumber { get; set; }
        //氏名
        public string stname { get; set; }
        //コース
        public string course { get; set; }
        //クラス
        public string clas { get; set; }
        //メールアドレス
        public string maill { get; set; }

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
                    open();
                    this.Close();
                }
            }
            else
            {
                open();
                this.Close();
            }
        }

        public void open()
        {
            MainWindow main = new MainWindow();

            main.teachername = this.teachername;

            main.Show();
        }


        private void btEdit_Click(object sender, RoutedEventArgs e)
        {
            DataRegistration registration = new DataRegistration();

            registration.teachername = this.teachername;

            registration.Show();
            this.Close();
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

            if (!string.IsNullOrWhiteSpace(stunumber)||
                !string.IsNullOrWhiteSpace(stname)||
                !string.IsNullOrWhiteSpace(course)||
                !string.IsNullOrWhiteSpace(clas)||
                !string.IsNullOrWhiteSpace(maill))
            {
                btTouroku.IsEnabled = false;
                btUpdate.IsEnabled = true;

                tbgakuseki.Text = this.stunumber;
                tbName.Text = this.stname;

                //コースを格納する
                for (int i = 0; i < cbCourse.Items.Count; i++)
                {
                    if (cbCourse.Items[i].ToString() == this.course )
                    {
                        cbCourse.SelectedItem = cbCourse.Items[i];
                        break;
                    }
                }

                tbClass.Text = this.clas;

                //メールアドレスを表示

                var maillad = maill;

                string[] mei = maillad.Split('@');

                tbMaill.Text = mei[0];


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

                MessageBoxResult result = MessageBox.Show("データを登録しました。"
                   , "警告", MessageBoxButton.OK);
                if (result == MessageBoxResult.OK)
                {
                    tbgakuseki.Text = "";
                    tbName.Text = "";
                    tbClass.Text = "";
                    tbMaill.Text = "";
                }
            }
        }
    }
}
