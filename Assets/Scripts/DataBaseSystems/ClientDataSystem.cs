namespace SQLDataBaseEditor
{
    using System;
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

        private const string FIRST_NAME_KEY = "FIRSTNAME";
        private const string FIRST_NAME_VALUE = "@FirstName";

        private const string LAST_NAME_KEY = "LASTNAME";
        private const string LAST_NAME_VALUE = "@LastName";

        private const string CARD_CODE_KEY = "CARDCODE";
        private const string CARD_CODE_VALUE = "@CardCode";

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
                    string query = GetNewClientCommand();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue(LAST_NAME_VALUE, row.Cells[LAST_NAME_KEY].Value);
                    command.Parameters.AddWithValue(FIRST_NAME_VALUE, row.Cells[FIRST_NAME_KEY].Value);
                    command.Parameters.AddWithValue(CARD_CODE_VALUE, row.Cells[CARD_CODE_KEY].Value);

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Ошибка при сохранении данных у таблицы в базу данных. Ex = {ex}");
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
                    string cardCode = clientNode.Attributes[CARD_CODE_KEY].Value;
                    string lastName = clientNode.Attributes[LAST_NAME_KEY].Value;
                    string firstName = clientNode.Attributes[FIRST_NAME_KEY].Value;

                    string query = GetNewClientCommand();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue(CARD_CODE_VALUE, cardCode);
                    command.Parameters.AddWithValue(LAST_NAME_VALUE, lastName);
                    command.Parameters.AddWithValue(FIRST_NAME_VALUE, firstName);

                    command.ExecuteNonQuery();
                }
            }
        }

        private string GetNewClientCommand() =>
            $"insert into {CLIENTS_TABLE} ({CARD_CODE_KEY}, {LAST_NAME_KEY}, {FIRST_NAME_KEY}) values ({CARD_CODE_VALUE}, {LAST_NAME_VALUE}, {FIRST_NAME_VALUE})";
    }
}
