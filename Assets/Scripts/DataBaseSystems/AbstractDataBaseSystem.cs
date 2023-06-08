namespace SQLDataBaseEditor
{
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// Абстрактная система базы данных
    /// </summary>
    public abstract class AbstractDataBaseSystem
    {
        public string Id { get; protected set; } = string.Empty;

        public AbstractDataBaseSystem(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Сохранение данных из грида в базе данных
        /// </summary>
        public abstract void SaveDataBaseSystem(DataGridView dataGridView);

        /// <summary>
        /// Загрузка информации с базы данных
        /// </summary>
        public abstract DataTable LoadDataBaseSystem();

        /// <summary>
        /// Удаляет все данные у таблицы
        /// </summary>
        public abstract void DeleteAllData();

        /// <summary>
        /// Загрузка данных в базу через импорт xml файла
        /// </summary>
        /// <param name="xmlFilePath"></param>
        public abstract void ImportXMLFileToDataBase(string xmlFilePath);
    }
}
