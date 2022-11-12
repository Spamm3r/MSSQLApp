using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace MSSQLApp.ViewModels
{
    public class MainViewModel: ObservableObject
    {
        #region Enum
        public enum OperationEnum
        {
            Test = 0,
            Fetch = 1
        }
        #endregion

        public MainViewModel()
        {
            this.BtnOperationCommand = new RelayCommand<int>(this.OperationExecute);
        }

        #region Commands
        public IRelayCommand<int> BtnOperationCommand { get; set; }
        #endregion

        #region Properties
        public Action onFailClearPassword;

        private string login;
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string ConnectionString
        {
            get => "Server=localhost;Database=DevData;Trusted_Connection=True;User=" + this.Login + ";Password=" + this.Password + ";Integrated Security=False;";
        }

        public ObservableCollection<UnionedItem> Query { get; set; } = new ObservableCollection<UnionedItem>();
        #endregion

        #region Methods
        /// <summary>
        /// Method Fetches or Tests connection with DataBase
        /// </summary>
        /// <param name="Argument">
        /// OperationEnum.Test - Tests if connection with DB can be made
        /// OperationEnum.Fetch - Fetches data (union of all int columns)
        /// </param>
        private void OperationExecute(int Argument)
        {
            if (Argument == (int)OperationEnum.Test)
            {
                try
                {
                    //simple connection checking
                    bool isConnection = false;
                    using(var con = new SqlConnection(this.ConnectionString))
                    {
                        con.Open();
                        isConnection = true;
                        con.Close();
                    }

                    //okay we got connection
                    if(isConnection) MessageBox.Show("Connection Open !");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error while trying to Connect! " + Environment.NewLine +  ex.Message);
                    this.Login = string.Empty;
                    this.Password = string.Empty;
                    this.onFailClearPassword.Invoke();
                }
            }
            else if (Argument == (int)OperationEnum.Fetch)
            {
                try
                {
                    //for long connections can be downloaded inside task
                    //and then BusyIndicator (optional) can be used to not freeze UI
                    //BusyIndicator is from package MyToolkit.Extender or for example from Telerik
                    this.Query.Clear();
                    using (var con = new SqlConnection(this.ConnectionString))
                    {
                        con.Open();
                        string sql = "SELECT Col_A1, '', '', '' FROM Table_A\n" +
                                      "UNION SELECT '', Col_B1, '', '' FROM Table_B\n" +
                                      "UNION SELECT '', '', Col_B3, '' FROM Table_B\n" +
                                      "UNION SELECT '', '', '', Col_C3 FROM Table_C";

                        using (SqlCommand command = new SqlCommand(sql, con))
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                this.Query.Add(new UnionedItem(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3)));
                            }
                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while trying to fetch data!\n" + ex.Message);
                    this.Login = string.Empty;
                    this.Password = string.Empty;
                    this.onFailClearPassword.Invoke();
                }
            }
        }
        #endregion
    }

    public class UnionedItem
    {
        public UnionedItem(int Col_A1, int Col_B1, int Col_B3, int Col_C3)
        {
            if (Col_A1 != 0) this.Col_A1 = Col_A1;
            if (Col_B1 != 0) this.Col_B1 = Col_B1;
            if (Col_B3 != 0) this.Col_B3 = Col_B3;
            if (Col_C3 != 0) this.Col_C3 = Col_C3;
        }

        #region Properties
        public int Col_A1 { get; set; } = 0;
        public int Col_B1 { get; set; } = 0;
        public int Col_B3 { get; set; } = 0;
        public int Col_C3 { get; set; } = 0;
        #endregion
    }
}
