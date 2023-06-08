namespace SQLDataBaseEditor
{
    /// <summary>
    /// Расщирения комманд для рабо ты с MS SQL
    /// </summary>
    public static class SQLCommandExtensions
    {
        /// <summary>
        /// Возвращает команду данных о таблице
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetDataTable(string table) => $"SELECT * FROM {table}";

        /// <summary>
        /// Удаляет данные у таблицы
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DeleteDataTable(string table) => $"DELETE FROM {table}";
    }
}
