namespace SQLDataBaseEditor
{

    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clientButton = new System.Windows.Forms.Button();
            this.cardButton = new System.Windows.Forms.Button();
            this.dataBaseLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clientButton
            // 
            this.clientButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clientButton.Location = new System.Drawing.Point(289, 100);
            this.clientButton.Name = "clientButton";
            this.clientButton.Size = new System.Drawing.Size(226, 84);
            this.clientButton.TabIndex = 0;
            this.clientButton.Text = "Client";
            this.clientButton.UseVisualStyleBackColor = true;
            this.clientButton.Click += new System.EventHandler(this.ClientFormOpen_ButtonClick);
            // 
            // cardButton
            // 
            this.cardButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cardButton.Location = new System.Drawing.Point(289, 212);
            this.cardButton.Name = "cardButton";
            this.cardButton.Size = new System.Drawing.Size(226, 84);
            this.cardButton.TabIndex = 1;
            this.cardButton.Text = "Card";
            this.cardButton.UseVisualStyleBackColor = true;
            this.cardButton.Click += new System.EventHandler(this.CardFormOpen_ButtonClick);
            // 
            // dataBaseLabel
            // 
            this.dataBaseLabel.AutoSize = true;
            this.dataBaseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataBaseLabel.Location = new System.Drawing.Point(261, 26);
            this.dataBaseLabel.Name = "dataBaseLabel";
            this.dataBaseLabel.Size = new System.Drawing.Size(282, 38);
            this.dataBaseLabel.TabIndex = 2;
            this.dataBaseLabel.Text = "Choose data base";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataBaseLabel);
            this.Controls.Add(this.cardButton);
            this.Controls.Add(this.clientButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button clientButton;
        private System.Windows.Forms.Button cardButton;
        private System.Windows.Forms.Label dataBaseLabel;
    }
}