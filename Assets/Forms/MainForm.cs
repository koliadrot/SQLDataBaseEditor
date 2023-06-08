namespace SQLDataBaseEditor
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Главное окно с выбором работы базы данных
    /// </summary>
    public partial class MainForm : Form
    {
        private string DATA_BASE = @"Data Source=DESKTOP-8O0TOOS\SQLTEST;Initial Catalog=DataBase;Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();
        }
        private void ClientFormOpen_ButtonClick(object sender, EventArgs e)
        {
            ClientDataSystem clientDataSystem = new ClientDataSystem(DATA_BASE, nameof(ClientDataSystem));
            OpenDataBaseForm(clientDataSystem);
        }

        private void CardFormOpen_ButtonClick(object sender, EventArgs e)
        {
            CardDataSystem cardDataSystem = new CardDataSystem(DATA_BASE, nameof(CardDataSystem));
            OpenDataBaseForm(cardDataSystem);
        }

        private void OpenDataBaseForm(AbstractDataBaseSystem dataSystem)
        {
            DataForm cardForm = new DataForm(dataSystem, this);
            cardForm.Show();
            cardForm.FormClosed += QuitApplication;
            Hide();
        }

        private void QuitApplication(object sender, FormClosedEventArgs e) => Close();
    }
}
