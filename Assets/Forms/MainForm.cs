namespace SQLDataBaseEditor
{
    using Newtonsoft.Json;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Главное окно с выбором работы базы данных
    /// </summary>
    public partial class MainForm : Form
    {
        private DataBase data;
        private const string CONFIG_NAME = "DataBaseConfigs.json";

        public MainForm()
        {
            InitConfigs();
            InitializeComponent();
        }

        private void InitConfigs()
        {
            string pathJsonConfig = Path.Combine(Application.StartupPath, CONFIG_NAME);
            if (!File.Exists(pathJsonConfig))
            {
                Debug.WriteLine($"Отсутствует файл конфигурации {CONFIG_NAME}");
            }
            else
            {
                string json = File.ReadAllText(pathJsonConfig);
                data = JsonConvert.DeserializeObject<DataBase>(json);
            }
        }

        private void ClientFormOpen_ButtonClick(object sender, EventArgs e)
        {
            if (data == null)
            {
                Debug.WriteLine($"Отсутствует файл конфигурации {CONFIG_NAME}");
            }
            else
            {
                DataTableSystem clientDataSystem = new DataTableSystem(data.Clients);
                OpenDataBaseForm(clientDataSystem);
            }
        }

        private void CardFormOpen_ButtonClick(object sender, EventArgs e)
        {
            if (data == null)
            {
                Debug.WriteLine($"Отсутствует файл конфигурации {CONFIG_NAME}");
            }
            else
            {
                DataTableSystem cardDataSystem = new DataTableSystem(data.Cards);
                OpenDataBaseForm(cardDataSystem);
            }
        }

        private void OpenDataBaseForm(DataTableSystem dataSystem)
        {
            DataForm cardForm = new DataForm(dataSystem);
            cardForm.Show();
            cardForm.FormClosed += AutoShowForm;
            Hide();
        }

        private void AutoShowForm(object sender, FormClosedEventArgs e) => Show();
    }
}
