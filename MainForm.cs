namespace SQLDataBaseEditor
{
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private string CONNECTION_DATA = @"Data Source=DESKTOP-8O0TOOS\SQLTEST;Initial Catalog=ClientsBase;Integrated Security=True";

        private ClientSystem clientSystem;

        public MainForm()
        {
            clientSystem = new ClientSystem(CONNECTION_DATA);
            InitializeComponent();
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) => clientSystem.SaveChangesToDatabase(dataGridView);
        private void MainForm_Load(object sender, System.EventArgs e) => DrawGridClients();

        private void DrawGridClients()
        {
            var data = clientSystem.LoadDataFromDatabase();
            dataGridView.DataSource = data;
        }

        private void ImportDataToDataBase(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = StaticData.XML_FILTER;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xmlFilePath = openFileDialog.FileName;
                clientSystem.ImportXMLFileToDataBase(xmlFilePath);
                DrawGridClients();
            }
        }
    }
}
