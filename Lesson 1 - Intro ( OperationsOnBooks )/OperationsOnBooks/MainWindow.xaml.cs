using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace OperationsOnBooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Fields

        string connectionString = "Data Source=DESKTOP-0V84BDI\\SQLEXPRESS;Initial catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        SqlConnection? connection;
        SqlCommand? command;
        SqlDataReader? reader;

        #endregion


        #region ConnectionToLibrary

        private void LoadNameOfAuthors(string categoryName)
        {
            try
            {
                //  Create connection 
                connection = new SqlConnection(connectionString);
                connection.Open();

                //  Create command
                string query = "SELECT FirstName FROM Authors\r\nINNER JOIN Books ON Authors.Id = Books.Id_Author\r\nINNER JOIN Categories ON Categories.Id = Books.Id_Category\r\nWHERE Categories.Name = @name";
                command = new SqlCommand(query, connection);

                reader = command.ExecuteReader();

                //  Dispose fields
                
                command.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                connection!.Close();
            }
        }

        #endregion
    }
}
