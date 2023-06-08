namespace SQLDataBaseEditor
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Система работы таблицы с базой данных SQL
    /// </summary>
    public class DataTableSystem
    {
        private readonly TableData sectionData;

        public DataTableSystem(TableData newSectionData) => sectionData = newSectionData;

        /// <summary>
        /// Id рабочей таблицы
        /// </summary>
        public string Id => sectionData == null ? string.Empty : sectionData.Id;

        /// <summary>
        /// Сохранение данных из грид таблицы в базу данных
        /// </summary>
        /// <param name="dataGridView"></param>
        //NOTE:Возможно стоит переделать систему сохранения под каждую ячейку.Зависит от UI.
        public void SaveDataBaseSystem(DataGridView dataGridView)
        {
            using (SqlConnection connection = new SqlConnection(sectionData.DataBaseSettings))
            {
                connection.Open();
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    string query = GetNewClientCommand(sectionData.Entries);

                    SqlCommand command = new SqlCommand(query, connection);
                    foreach (var kvp in sectionData.Entries)
                    {
                        string key = kvp.Key;
                        string value = kvp.Value;
                        command.Parameters.AddWithValue(value, row.Cells[key].Value);
                    }

                    //FIXME:Надо по другому проверять пустые значения, иначе оптимизация хромать будет.
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

        /// <summary>
        /// Загрузка данных о таблийце из базы данных
        /// </summary>
        /// <returns></returns>
        public DataTable LoadDataBaseSystem()
        {
            using (SqlConnection connection = new SqlConnection(sectionData.DataBaseSettings))
            {
                string query = SQLCommandExtensions.GetDataTable(sectionData.Table);
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, sectionData.Table);

                return dataset.Tables[sectionData.Table];
            }
        }

        /// <summary>
        /// Удаляет все данные у таблицы
        /// </summary>
        public void DeleteAllData()
        {
            using (SqlConnection connection = new SqlConnection(sectionData.DataBaseSettings))
            {
                string query = SQLCommandExtensions.DeleteDataTable(sectionData.Table);
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Импорт в базу данных из файла xml
        /// </summary>
        /// <param name="xmlFilePath"></param>
        public void ImportXMLFileToDataBase(string xmlFilePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            using (SqlConnection connection = new SqlConnection(sectionData.DataBaseSettings))
            {
                connection.Open();
                foreach (XmlNode clientNode in xmlDoc.SelectNodes(sectionData.RootPath))
                {
                    SqlCommand command = new SqlCommand(GetNewClientCommand(sectionData.Entries), connection);

                    foreach (KeyValuePair<string, string> kvp in sectionData.Entries)
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

            return $"insert into {sectionData.Table} ({columns}) values ({values})";
        }
    }
}
