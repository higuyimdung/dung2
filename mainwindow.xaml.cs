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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;
using Microsoft.Win32;
namespace higuyimdung
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ConnectionString = "Data Source = D:\\SOL.db";
        SQLiteConnection conn = new SQLiteConnection(ConnectionString);
        public MainWindow()
        {
            InitializeComponent();
            loadData();
        }



        public void ClearData()
        {
            ID_txt.Clear();
            FileName_txt.Clear();
            Path_txt.Clear();
            SQATime_txt.Clear();
            UpdateTime_txt.Clear();
        }
        private void Clearbtn_Click_1(object sender, RoutedEventArgs e)
        {
            ClearData();
        }



        public void loadData()
        {
            conn.Open();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter();
            SQLiteCommand cmd = new SQLiteCommand();
            DataTable dt = new DataTable();
            string query = "Select * from SOL1";
            cmd.CommandText = query;
            adapter.SelectCommand = cmd;
            cmd.Connection = conn;
            SQLiteDataReader reader = cmd.ExecuteReader();
            dt.Load(reader);
            conn.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
        public bool isValid()
        {
            if (ID_txt.Text == String.Empty)
            {
                MessageBox.Show("ID is required", "Failer", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (FileName_txt.Text == String.Empty)
            {
                MessageBox.Show("FileName is required", "Failer", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Path_txt.Text == String.Empty)
            {
                MessageBox.Show("Path is required", "Failer", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (SQATime_txt.Text == String.Empty)
            {
                MessageBox.Show("SQATime is required", "Failer", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (UpdateTime_txt.Text == String.Empty)
            {
                MessageBox.Show("UpdateTime is required", "Failer", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
        private void Insertbtn_Click_1(object sender, RoutedEventArgs e)
        {



            try
            {
                if (isValid())
                {
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand("INSERT INTO SOL1 VALUES (@ID, @FileNAme, @Path, @SQATime, @UpdateTime, @Version)");
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@ID", ID_txt.Text);
                    cmd.Parameters.AddWithValue("@FileName", FileName_txt.Text);
                    cmd.Parameters.AddWithValue("@Path", Path_txt.Text);
                    cmd.Parameters.AddWithValue("@SQATime", SQATime_txt.Text);
                    cmd.Parameters.AddWithValue("@UpdateTime", UpdateTime_txt.Text);
                    cmd.Parameters.AddWithValue("@Version", Version_txt.Text);
                    cmd.ExecuteReader();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadData();
                    MessageBox.Show("Successfully resigter", "saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Deletebtn_Click_1(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("delete from SOL1 where ID = " + ID_txt.Text + " ");
            try
            {
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                ClearData();
                loadData();
                conn.Close();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show("Not deleted" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void Uploadbtn_Click_1(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("update SOL1 set FileName = '" + FileName_txt.Text + "', Path ='" + Path_txt.Text + "',SQATime = '" + SQATime_txt.Text + "',UpdateTime='" + UpdateTime_txt.Text + "',Version = '" + Version_txt.Text + "'WHERE ID ='" + ID_txt.Text + "'");
            try
            {
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been updated successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                ClearData();
                loadData();
            }

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
               
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM SOL1 where ID = " + ID_txt.Text + " ");
                DataTable dt = new DataTable();
                conn.Open();
                cmd.Connection = conn;
                SQLiteDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                datagrid.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }*/
            Window2 win = new Window2();
            win.Show();
            win.hien(ID_txt.Text);
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (openFileDialog.ShowDialog() == true)
                {
                    SQLiteCommand cmd2 = new SQLiteCommand("SELECT ID FROM SOL1");
                    DataTable dt = new DataTable();
                    conn.Open();
                    cmd2.Connection = conn;
                    SQLiteDataReader reader = cmd2.ExecuteReader();
                    dt.Load(reader);
                    int n = dt.Rows.Count + 1;
                    conn.Close();
                    try
                    {
                        foreach (string filename in openFileDialog.FileNames)
                        {
                            SQLiteCommand cmd = new SQLiteCommand("INSERT INTO SOL1 VALUES (@ID,@FileName, @Path, @SQAtime, @UpdateTime , @Version)");
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = conn;
                            cmd.Parameters.AddWithValue("@ID", n);
                            n++;
                            cmd.Parameters.AddWithValue("@FileName", System.IO.Path.GetFileName(filename));
                            cmd.Parameters.AddWithValue("@Path", filename);
                            String s = System.IO.Path.GetFileName(filename);
                            String s1 = s.Substring(3, 8);
                            String s2 = s.Substring(12, 2);
                            //String s3 = s2.Insert(2, "/");
                            cmd.Parameters.AddWithValue("@SQAtime", s1);
                            cmd.Parameters.AddWithValue("@UpdateTime", s2);
                            cmd.Parameters.AddWithValue("@Version", "1");
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            loadData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        MessageBox.Show("Successfully resigter", "saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    
}

