﻿namespace SQLDataBaseEditor
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Главное окно с выбором работы базы данных
    /// </summary>
    public partial class MainForm : Form
    {
        //TODO:Вынести в отдельный файл и парсить оттуда данные для подключения к базе
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
            cardForm.FormClosed += AutoShowForm;
            Hide();
        }

        private void AutoShowForm(object sender, FormClosedEventArgs e) => Show();
    }
}
