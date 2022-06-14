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
using System.Data.SQLite;
using System.Data;
namespace higuyimdung
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private bool allowLogin()
        {
            if(username.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }    
            if(password.Password.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }
        private void Exit_txt_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void login_txt_Click(object sender, RoutedEventArgs e)
        {
            if (!allowLogin())
            { return; }
                MainWindow main = new MainWindow();
                this.Close();
                main.Show();
            
            //DataTable dtData = Connection
        }
    }
}
