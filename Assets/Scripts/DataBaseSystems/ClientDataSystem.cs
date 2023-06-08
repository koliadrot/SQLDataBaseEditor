namespace SQLDataBaseEditor
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Система клиентской базы данных
    /// </summary>
    public class ClientDataSystem : AbstractDataBaseSystem
    {
        private readonly string connectionSettings;

        private const string ROOT_PATH = "/Clients/Client";

        private const string CLIENTS_TABLE = "Clients";

        //NOTE:Возможно стоит из файла xml примера парсить ключи
        private Dictionary<string, string> dataClients = new Dictionary<string, string>()
        {
            { "CARDCODE", "@CardCode" },
            { "STARTDATE", "@StartDate" },
            { "FINISHDATE", "@FinishDate" },
            { "LASTNAME", "@LastName" },
            { "FIRSTNAME", "@FirstName" },
            { "SURNAME", "@Surname" },
            { "GENDER", "@Gender" },
            { "BIRTHDAY", "@Birthday" },
            { "PHONEHOME", "@PhoneHome" },
            { "PHONEMOBIL", "@PhoneMobil" },
            { "EMAIL", "@Email" },
            { "CITY", "@City" },
            { "STREET", "@Street" },
            { "HOUSE", "@House" },
            { "APARTMENT", "@Apartment" }
        };

        public ClientDataSystem(string connectionDataBase, string id) : base(id)
        {
            connectionSettings = connectionDataBase;
        }

        //NOTE:Возможно стоит переделать систему сохранения под каждую ячейку.Зависит от UI.
        public override void SaveDataBaseSystem(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(connectionSettings))
            {
                connection.Open();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    string query = GetNewClientCommand(dataClients);

                    SqlCommand command = new SqlCommand(query, connection);
                    foreach (var kvp in dataClients)
                    {
                        string key = kvp.Key;
                        string value = kvp.Value;
                        command.Parameters.AddWithValue(value, row.Cells[key].Value);
                    }

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Ошибка при сохранении данных таблицы в базу данных. Ex = {ex}");
                    }
                }
            }
        }

        public override DataTable LoadDataBaseSystem()
        {
            using (SqlConnection connection = new SqlConnection(connectionSettings))
            {
                string query = SQLCommandExtensions.GetDataTable(CLIENTS_TABLE);
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, CLIENTS_TABLE);

                return dataset.Tables[CLIENTS_TABLE];
            }
        }

        public override void DeleteAllData()
        {
            using (SqlConnection connection = new SqlConnection(connectionSettings))
            {
                string query = SQLCommandExtensions.DeleteDataTable(CLIENTS_TABLE);
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public override void ImportXMLFileToDataBase(string xmlFilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            using (SqlConnection connection = new SqlConnection(connectionSettings))
            {
                connection.Open();
                foreach (XmlNode clientNode in xmlDoc.SelectNodes(ROOT_PATH))
                {
                    SqlCommand command = new SqlCommand(GetNewClientCommand(dataClients), connection);

                    foreach (KeyValuePair<string, string> kvp in dataClients)
                    {
                        string attributeValue = clientNode.Attributes[kvp.Key].Value;
                        command.Parameters.AddWithValue(kvp.Value, attributeValue);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        private string GetNewClientCommand(Dictionary<string, string> dataClients)
        {
            string columns = string.Join(", ", dataClients.Keys);
            string values = string.Join(", ", dataClients.Values);

            return $"insert into {CLIENTS_TABLE} ({columns}) values ({values})";
        }
    }
}
