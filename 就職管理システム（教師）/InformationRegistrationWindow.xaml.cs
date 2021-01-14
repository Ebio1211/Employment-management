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
    /// InformationRegistrationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class InformationRegistrationWindow : Window
    {
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
    }
}
