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

        public string stunumber { get; set; }

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

        //企業情報の登録
        就職管理システム_教師_.RecruitManagementDataBaseDataSetTableAdapters.
            RecruitTableTableAdapter recruitTable =
            new RecruitManagementDataBaseDataSetTableAdapters.RecruitTableTableAdapter();

        public DataRegistration()
        {
            InitializeComponent();
        }

        private void btReturn_Click(object sender, RoutedEventArgs e)
        {
            InformationRegistrationWindow information = new InformationRegistrationWindow();

            information.teachername = this.teachername;

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
            recruitTable.Fill(recruitManagement.RecruitTable);

            //int remdata = dgStudentsData.SelectedIndex;
            //var datar = (DataRow)recruitManagement.StudentTable.Rows[remdata];

            var data = (DataRow)dgStudentsData.SelectedItem;

            stunumber = data[0].ToString();

            var Delete = recruitManagement.RecruitTable.AsEnumerable().Count(
                    d => d.StudenNumber.ToString().Contains(stunumber)
                    );

            if (Delete > 0)
            {
#if false
                MessageBoxResult result = MessageBox.Show("該当生徒の就活データデータを削除しますか？"
                  , "警告", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {

                    var clear = recruitManagement.RecruitTable.AsEnumerable().Where(
                        d => d.StudenNumber.ToString().Contains(stunumber)
                        ).Select(s => s.RecruitID).ToList();
                    try
                    {

                        foreach (var cldata in clear)
                        {
                            if (cldata == 0)
                            {
                                var datar = (DataRow)recruitManagement.RecruitTable.Rows[cldata];
                                datar.Delete();
                                //データベース更新
                                studentTableTable.Adapter.Update(recruitManagement.StudentTable);
                            }
                            else
                            {
                                var datar = (DataRow)recruitManagement.RecruitTable.Rows[cldata - 1];
                                datar.Delete();
                                //データベース更新
                                studentTableTable.Adapter.Update(recruitManagement.StudentTable);
                            }
                        }

                        MessageBox.Show("就活データを削除しました。", "警告", MessageBoxButton.OK);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("削除に失敗しました。" + "\n" + ex.Message);
                    }


                    try
                    {
                        MessageBoxResult stresult = MessageBox.Show("データを削除しますか？"
                          , "警告", MessageBoxButton.YesNo);
                        if (stresult == MessageBoxResult.Yes)
                        {
                            int remdata = dgStudentsData.SelectedIndex;

                            var datar = (DataRow)recruitManagement.StudentTable.Rows[remdata];

                            //datar.Delete();



                            //データベース更新
                            studentTableTable.Adapter.Update(recruitManagement.StudentTable);
                            Window_Loaded(sender, e);
                            MessageBox.Show("生徒データを削除しました。", "警告", MessageBoxButton.OK);
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("削除に失敗しました。" + "\n" + ex.Message);
                    }


                }
#endif
                MessageBox.Show("就活データがあるので削除できません。", "警告", MessageBoxButton.OK);
            }
            else if(Delete == 0)
            {
                try
                {
                    MessageBoxResult stresult = MessageBox.Show("データを削除しますか？"
                      , "警告", MessageBoxButton.YesNo);
                    if (stresult == MessageBoxResult.Yes)
                    {
                        int remdata = dgStudentsData.SelectedIndex;

                        var datar = (DataRow)recruitManagement.StudentTable.Rows[remdata];

                        datar.Delete();



                        //データベース更新
                        studentTableTable.Adapter.Update(recruitManagement.StudentTable);
                        Window_Loaded(sender, e);
                        MessageBox.Show("生徒データを削除しました。", "警告", MessageBoxButton.OK);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("削除に失敗しました。" + "\n" + ex.Message);
                }
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
