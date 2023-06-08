namespace SQLDataBaseEditor
{
    using System;

    /// <summary>
    /// База данных с таблицами
    /// </summary>
    [Serializable]
    public class DataBase
    {
        public TableData Clients;
        public TableData Cards;
    }
}
