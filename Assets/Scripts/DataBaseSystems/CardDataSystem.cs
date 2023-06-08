namespace SQLDataBaseEditor
{
    using System.Data;
    using System.Windows.Forms;

    public class CardDataSystem : AbstractDataBaseSystem
    {
        private readonly string connectionSettings;

        private const string ROOT_PATH = "/Cards/Card";

        private const string CARDS_TABLE = "Cards";
        private const string FIRST_NAME_KEY = "FIRSTNAME";
        private const string FIRST_NAME_VALUE = "@FirstName";
        private const string LAST_NAME_KEY = "LASTNAME";
        private const string LAST_NAME_VALUE = "@LastName";
        private const string CARD_CODE_KEY = "CARDCODE";
        private const string CARD_CODE_VALUE = "@CardCode";

        public CardDataSystem(string connectionDataBase, string id) : base(id)
        {
            connectionSettings = connectionDataBase;
        }

        public override void ImportXMLFileToDataBase(string xmlFilePath) { }
        public override DataTable LoadDataBaseSystem()
        {
            return default;
        }

        public override void SaveDataBaseSystem(DataGridView dataGridView)
        {
        }
    }
}