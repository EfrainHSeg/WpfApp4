using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Lógica de interacción para clientes.xaml
    /// </summary>
    public partial class clientes : Window
    {

        string connectionString = "Data Source=LAPTOP-PUU85PCD\\SQLEXPRESS;Initial Catalog=NeptunoDB;User ID=USER02;Password=12345;";
        List<Cliente> clientesList = new List<Cliente>();
        public clientes()
        {
            InitializeComponent();
            LoadClientesData();
            
        }

        private void LoadClientesData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT idCliente, NombreCompañia, NombreContacto, CargoContacto FROM clientes";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string IdCliente = reader.GetString("IdCliente");
                                string NombreCompañia = reader.GetString("NombreCompañia");
                                string NombreContacto = reader.GetString("NombreContacto");
                                string CargoContacto = reader.GetString("CargoContacto");

                                clientesList.Add(new Cliente { IdCliente = IdCliente, NombreCompañia = NombreCompañia, NombreContacto = NombreContacto, CargoContacto = CargoContacto });
                            }


                        }
                    }
                }
                clientesGrid.ItemsSource= clientesList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos de clientes: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
