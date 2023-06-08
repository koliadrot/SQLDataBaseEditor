namespace SQLDataBaseEditor
{
    using System.Windows.Forms;

    /// <summary>
    /// Форма с базой данных
    /// </summary>
    public partial class DataForm : Form
    {
        private DataTableSystem dataSystem;
        private const string XML_FILTER = "XML Files (*.xml)|*.xml";

        public DataForm(DataTableSystem abstractDataSystem)
        {
            dataSystem = abstractDataSystem;
            InitializeComponent();
            dataLabel.Text = dataSystem.Id;
        }

        private void DataForm_Load(object sender, System.EventArgs e) => DrawGridData();

        private void SaveDataOnDataBase_ButtonClick(object sender, System.EventArgs e)
        {
            dataSystem.DeleteAllData();
            dataSystem.SaveDataBaseSystem(dataGridView);
        }

        private void LoadDataFromDataBase_ButtonClick(object sender, System.EventArgs e) => DrawGridData();

        private void DrawGridData()
        {
            var data = dataSystem.LoadDataBaseSystem();
            dataGridView.DataSource = data;
        }

        private void ImportXMLFileToDataBase(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = XML_FILTER;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xmlFilePath = openFileDialog.FileName;
                dataSystem.ImportXMLFileToDataBase(xmlFilePath);
                DrawGridData();
            }
        }

        private void Back_ButtonClick(object sender, System.EventArgs e) => Close();
    }
}
