namespace SQLDataBaseEditor
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Информация конкретной таблицы
    /// </summary>
    [Serializable]
    public class TableData
    {
        public string Id;
        public string RootPath;
        public string Table;
        public string DataBaseSettings;
        public Dictionary<string, string> Entries = new Dictionary<string, string>();
    }
}
