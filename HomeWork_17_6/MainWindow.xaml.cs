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
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;

namespace HomeWork_17_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlConnection;
        OleDbConnection oleDbConnection;
        DataTable dt1;
        DataTable dt2;
        DataRowView row;

        SqlDataAdapter sqlDataAdapter;
        OleDbDataAdapter oleDbDataAdapter;
        public MainWindow()
        {
            InitializeComponent();
            StartBd();
            
        }
  
       
        private void StartBd()
        {
            #region install
            SqlConnectionStringBuilder stringBuilder1 = new SqlConnectionStringBuilder()
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                AttachDBFilename = @"C:\C\SQLDB.mdf",
                IntegratedSecurity = true
            };
            OleDbConnectionStringBuilder stringBuilder2 = new OleDbConnectionStringBuilder()
            {
                DataSource = @"C:\C\AccessDB.accdb",
                PersistSecurityInfo = true,
                Provider = "Microsoft.ACE.OLEDB.12.0"
            };
           
            sqlConnection = new SqlConnection(stringBuilder1.ConnectionString);
            oleDbConnection = new OleDbConnection(stringBuilder2.ConnectionString);
            
            sqlConnection.StateChange += (ob, ev) => { Debug.WriteLine(nameof(sqlConnection) + $" {(ob as SqlConnection).State}"); };
            oleDbConnection.StateChange += (ob, ev) => { Debug.WriteLine(nameof(oleDbConnection) + $" {(ob as OleDbConnection).State}"); };

            dt1 = new DataTable();
            dt2 = new DataTable();
            sqlDataAdapter = new SqlDataAdapter();
            oleDbDataAdapter = new OleDbDataAdapter();

            #endregion

            #region connect
            /*try
            {
                sqlConnection.Open();
                oleDbConnection.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                sqlConnection.Close();
                oleDbConnection.Close();
            }*/
            #endregion

            #region запросы 
            ////select
            string sql = @"SELECT * FROM Clients";
            sqlDataAdapter.SelectCommand = new SqlCommand(sql, sqlConnection);

            sql = @"SELECT * FROM Client";
            oleDbDataAdapter.SelectCommand = new OleDbCommand(sql, oleDbConnection);

            ///insert
            
            sql = @"INSERT INTO Clients (FirstName, LastName, MiddleName, Email, TelephonNumber)
                            VALUES (@FirstName, @LastName, @MiddleName, @Emailm @TelephonNumber)
                        SET @Id = @@IDENTITY;";
            sqlDataAdapter.InsertCommand = new SqlCommand(sql, sqlConnection);
            sqlDataAdapter.InsertCommand.Parameters.Add("@Id",SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
            sqlDataAdapter.InsertCommand.Parameters.Add("@FirstName", SqlDbType.VarChar, 20, "FirstName");
            sqlDataAdapter.InsertCommand.Parameters.Add("@LastName", SqlDbType.VarChar, 20, "LastName");
            sqlDataAdapter.InsertCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar, 20, "MiddleName");
            sqlDataAdapter.InsertCommand.Parameters.Add("@Email", SqlDbType.VarChar, 20, "Email");
            sqlDataAdapter.InsertCommand.Parameters.Add("@TelephonNumber", SqlDbType.Decimal, 10, "TelephonNumber");

            sql = @"INSERT INTO Client (Email, QrCode, Product)
                            VALUES (@Email, @QrCode, @Product)
                        SET @ID = @@IDENTITY;";
            oleDbDataAdapter.InsertCommand = new OleDbCommand(sql, oleDbConnection);
            oleDbDataAdapter.InsertCommand.Parameters.Add("@ID", OleDbType.Integer).Direction = ParameterDirection.Output;
            oleDbDataAdapter.InsertCommand.Parameters.Add("@Email", OleDbType.VarChar, 20, "Email");
            oleDbDataAdapter.InsertCommand.Parameters.Add("@QrCode", OleDbType.VarChar, 20, "QrCode");
            oleDbDataAdapter.InsertCommand.Parameters.Add("@Product", OleDbType.VarChar, 20, "Product");
            ////delete

            sql = "DELETE FROM Clients WHERE Id = @Id";
            sqlDataAdapter.DeleteCommand = new SqlCommand(sql, sqlConnection);
            sqlDataAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");

            sql = "DELETE FROM Client WHERE ID = @ID";
            oleDbDataAdapter.DeleteCommand = new OleDbCommand(sql, oleDbConnection);
            oleDbDataAdapter.DeleteCommand.Parameters.Add("ID", OleDbType.Integer, 4, "ID");
            /////update

            sql = @"UPDATE Clients SET 
                           FirstName = @FirstName,
                           LastName = @LastName, 
                           MiddleName = @MiddleName,
                           Email = @Email,
                           TelephonNumber = @TelephonNumber
                    WHERE Id = @Id";
            sqlDataAdapter.UpdateCommand = new SqlCommand(sql, sqlConnection);
            sqlDataAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id").SourceVersion = DataRowVersion.Original;
            sqlDataAdapter.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            sqlDataAdapter.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 20, "LastName");
            sqlDataAdapter.UpdateCommand.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 20, "MiddleName");
            sqlDataAdapter.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 20, "Email");
            sqlDataAdapter.UpdateCommand.Parameters.Add("@TelephonNumber", SqlDbType.Decimal, 10, "TelephonNumber");

            sql = @"UPDATE Client SET 
                        Email = @Email,
                        QrCode = @QrCode,
                        Product = @Product
                   WHERE ID = @ID";
            oleDbDataAdapter.UpdateCommand = new OleDbCommand(sql, oleDbConnection);
            oleDbDataAdapter.UpdateCommand.Parameters.Add("@ID", OleDbType.Integer, 0, "ID").SourceVersion = DataRowVersion.Original;
            oleDbDataAdapter.UpdateCommand.Parameters.Add("@Email", OleDbType.VarChar, 20, "Email");
            oleDbDataAdapter.UpdateCommand.Parameters.Add("@QrCode", OleDbType.VarChar, 20, "QrCode");
            oleDbDataAdapter.UpdateCommand.Parameters.Add("@Product", OleDbType.VarChar, 20, "Product");

            sqlDataAdapter.Fill(dt1);
            oleDbDataAdapter.Fill(dt2);

            gridClienS.DataContext = dt1.DefaultView;
            gridClienA.DataContext = dt2.DefaultView;
            #endregion

        }

    }
}
