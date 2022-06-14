using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace higuyimdung
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public const string ConnectionString = "Data Source = D:\\SOL.db";
        SQLiteConnection conn = new SQLiteConnection(ConnectionString);
        public Window2()
        {
            InitializeComponent();
        }
        public void hien(string id)
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM SOL1 where ID = " + id + " ");
            DataTable dt = new DataTable();
            conn.Open();
            cmd.Connection = conn;
            SQLiteDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            ID1.Text = dt.Rows[0]["ID"].ToString();
            FileName1.Text = dt.Rows[0]["FileName"].ToString();
            Path1.Text = dt.Rows[0]["Path"].ToString();
            SQATime1.Text = dt.Rows[0]["SQAtime"].ToString();
            UpdateTime1.Text = dt.Rows[0]["UpdateTime"].ToString();
            Version1.Text = dt.Rows[0]["Version"].ToString();
            conn.Close();
            
        }
    }
}
