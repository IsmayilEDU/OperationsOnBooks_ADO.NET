using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            FillComboboxOfNamesOfCategories();
        }

        #region Fields

        //  ConnectionString for connection
        string connectionString = "Data Source=DESKTOP-0V84BDI\\SQLEXPRESS;Initial catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        
        //  Connection to database
        SqlConnection? connection;

        //  Names of categories for combobox
        public List<string> NamesOfCategories { get; set; }

        //  Full property Firstnames Of Authors for combobox
        private List<string> firstnamesOfAuthors;
        public List<string> FirstnamesOfAuthors { get => firstnamesOfAuthors; set
            {
                firstnamesOfAuthors = value;
                OnPropertyChanged();
            }
        }

        //  Selected Name Of Categories for search Firstnames of authors
        private string? SelectedNameOfCategories = null;

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region FunctionsOfWindow
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  Deactive combobox of authors
            combobox_Authors.IsEnabled = false;

        }
        private void combobox_Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedNameOfCategories = combobox_Categories.SelectedItem as string;
            if (SelectedNameOfCategories != null)
            {
                combobox_Authors.IsEnabled = true;
                FillComboboxOfComboboxOfFirstNamesOfAuthorsWithCategoryName(SelectedNameOfCategories);
            }
        }


        #endregion

        #region AssistentFunctions
        private void OnPropertyChanged([CallerMemberName] string? PrpertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PrpertyName));
        }
        private void FillComboboxOfNamesOfCategories()
        {
            //  Create connection 
            connection = new SqlConnection(connectionString);

            try
            {
                connection!.Open();

                //  Create command for load names of categories
                using SqlCommand command = new("SELECT Name FROM Categories\r\nGROUP BY Name", connection);

                //  Create reader for datas
                using SqlDataReader reader = command.ExecuteReader();

                //  Add datas to combobox
                NamesOfCategories = new List<string>();
                while (reader.Read())
                {
                    NamesOfCategories.Add((string)reader["Name"]);
                }
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

        private void FillComboboxOfComboboxOfFirstNamesOfAuthorsWithCategoryName(string categoryName)
        {
            try
            {
                connection!.Open();

                //  Create command for load names of categories
                using SqlCommand command = new($"SELECT FirstName FROM Authors\r\nINNER JOIN Books ON Books.Id_Author = Authors.Id\r\nINNER JOIN Categories ON Categories.Id = Books.Id_Category\r\nWHERE Categories.Name = '{categoryName}'\r\nGROUP BY FirstName", connection);

                //  Create reader for datas
                using SqlDataReader reader = command.ExecuteReader();

                //  Add datas to combobox
                FirstnamesOfAuthors = new List<string>();
                while (reader.Read())
                {
                    FirstnamesOfAuthors.Add((string)reader["FirstName"]);
                }
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
