namespace SQLDataBaseEditor
{
    using System.Windows.Forms;

    public partial class DataForm : Form
    {
        private AbstractDataBaseSystem dataSystem;
        private Form previousForm;

        public DataForm(AbstractDataBaseSystem abstractDataSystem, Form form)
        {
            previousForm = form;
            dataSystem = abstractDataSystem;
            InitializeComponent();
            dataLabel.Text = dataSystem.Id;
        }

        private void DataForm_Load(object sender, System.EventArgs e) => DrawGridData();

        private void SaveDataOnDataBase_ButtonClick(object sender, System.EventArgs e) => dataSystem.SaveDataBaseSystem(dataGridView);
        private void LoadDataFromDataBase_ButtonClick(object sender, System.EventArgs e) => DrawGridData();

        private void DrawGridData()
        {
            var data = dataSystem.LoadDataBaseSystem();
            dataGridView.DataSource = data;
        }

        private void ImportDataToDataBase(object sender, System.EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = StaticData.XML_FILTER;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string xmlFilePath = openFileDialog.FileName;
                dataSystem.ImportXMLFileToDataBase(xmlFilePath);
                DrawGridData();
            }
        }

        private void Back_ButtonClick(object sender, System.EventArgs e)
        {
            previousForm.Show();
            Close();
        }
    }
}
