namespace SQLDataBaseEditor
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Система клиентской базы данных
    /// </summary>
    public class ClientSystem
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

        public ClientSystem(string connectionDataBase)
        {
            connectionSettings = connectionDataBase;
        }


        /// <summary>
        /// Загрузка данных c базы данных
        /// </summary>
        /// <param name="dataGridView"></param>
        public DataTable LoadDataFromDatabase()
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

        /// <summary>
        /// Сохранение данных с грид таблицы в базу данных
        /// </summary>
        /// <param name="dataGridView"></param>
        public void SaveChangesToDatabase(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(connectionSettings))
            {
                string query = GetCommandDataClients(CARD_CODE_KEY, CARD_CODE_VALUE, FIRST_NAME_KEY, FIRST_NAME_VALUE, LAST_NAME_KEY, LAST_NAME_VALUE);

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue(LAST_NAME_VALUE, dataGridView.CurrentRow.Cells[LAST_NAME_KEY].Value);
                command.Parameters.AddWithValue(FIRST_NAME_VALUE, dataGridView.CurrentRow.Cells[FIRST_NAME_KEY].Value);
                command.Parameters.AddWithValue(CARD_CODE_VALUE, dataGridView.CurrentRow.Cells[CARD_CODE_KEY].Value);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Импорт клиентов в базу данных из xml файла
        /// </summary>
        public void ImportXMLFileToDataBase(string xmlFilePath)
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

                    string insertQuery = GetInsertDataClients(CARD_CODE_KEY, FIRST_NAME_KEY, LAST_NAME_KEY, CARD_CODE_VALUE, FIRST_NAME_VALUE, LAST_NAME_VALUE);

                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue(CARD_CODE_VALUE, cardCode);
                    command.Parameters.AddWithValue(LAST_NAME_VALUE, lastName);
                    command.Parameters.AddWithValue(FIRST_NAME_VALUE, firstName);

                    command.ExecuteNonQuery();
                }
            }
        }

        private string GetCommandDataClients(string cardCodeKey, string cardCodeValue, string firstNameKey, string firstNameValue, string lastNameKey, string lastNameValue) =>
            $"UPDATE Clients SET {lastNameKey} = {lastNameValue}, {firstNameKey} = {firstNameValue} WHERE {cardCodeKey} = {cardCodeValue}";

        private string GetInsertDataClients(string cardCodeKey, string firstNameKey, string lastNameKey, string cardCodeValue, string firstNameValue, string lastNameValue) =>
            $"INSERT INTO Clients ({cardCodeKey}, {lastNameKey}, {firstNameKey}) VALUES ({cardCodeValue}, {lastNameValue}, {firstNameValue})";
    }
}
