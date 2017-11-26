namespace TouristClient
{
    partial class AddInstructor
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
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.textBox_forename = new System.Windows.Forms.TextBox();
            this.textBox_surname = new System.Windows.Forms.TextBox();
            this.label_forename = new System.Windows.Forms.Label();
            this.label_surname = new System.Windows.Forms.Label();
            this.textBox_patronymic = new System.Windows.Forms.TextBox();
            this.label_patronymic = new System.Windows.Forms.Label();
            this.label_tourType = new System.Windows.Forms.Label();
            this.comboBox_tourType = new System.Windows.Forms.ComboBox();
            this.comboBox_tourDate = new System.Windows.Forms.ComboBox();
            this.label_tourDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(348, 147);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(267, 147);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 10;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // textBox_forename
            // 
            this.textBox_forename.Location = new System.Drawing.Point(103, 38);
            this.textBox_forename.Name = "textBox_forename";
            this.textBox_forename.Size = new System.Drawing.Size(321, 20);
            this.textBox_forename.TabIndex = 3;
            // 
            // textBox_surname
            // 
            this.textBox_surname.Location = new System.Drawing.Point(103, 12);
            this.textBox_surname.Name = "textBox_surname";
            this.textBox_surname.Size = new System.Drawing.Size(321, 20);
            this.textBox_surname.TabIndex = 1;
            // 
            // label_forename
            // 
            this.label_forename.AutoSize = true;
            this.label_forename.Location = new System.Drawing.Point(15, 41);
            this.label_forename.Name = "label_forename";
            this.label_forename.Size = new System.Drawing.Size(29, 13);
            this.label_forename.TabIndex = 2;
            this.label_forename.Text = "Имя";
            // 
            // label_surname
            // 
            this.label_surname.AutoSize = true;
            this.label_surname.Location = new System.Drawing.Point(15, 15);
            this.label_surname.Name = "label_surname";
            this.label_surname.Size = new System.Drawing.Size(56, 13);
            this.label_surname.TabIndex = 0;
            this.label_surname.Text = "Фамилия";
            // 
            // textBox_patronymic
            // 
            this.textBox_patronymic.Location = new System.Drawing.Point(103, 64);
            this.textBox_patronymic.Name = "textBox_patronymic";
            this.textBox_patronymic.Size = new System.Drawing.Size(321, 20);
            this.textBox_patronymic.TabIndex = 5;
            // 
            // label_patronymic
            // 
            this.label_patronymic.AutoSize = true;
            this.label_patronymic.Location = new System.Drawing.Point(15, 67);
            this.label_patronymic.Name = "label_patronymic";
            this.label_patronymic.Size = new System.Drawing.Size(54, 13);
            this.label_patronymic.TabIndex = 4;
            this.label_patronymic.Text = "Отчество";
            // 
            // label_tourType
            // 
            this.label_tourType.AutoSize = true;
            this.label_tourType.Location = new System.Drawing.Point(15, 96);
            this.label_tourType.Name = "label_tourType";
            this.label_tourType.Size = new System.Drawing.Size(51, 13);
            this.label_tourType.TabIndex = 6;
            this.label_tourType.Text = "Тип тура";
            // 
            // comboBox_tourType
            // 
            this.comboBox_tourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tourType.FormattingEnabled = true;
            this.comboBox_tourType.Location = new System.Drawing.Point(103, 93);
            this.comboBox_tourType.Name = "comboBox_tourType";
            this.comboBox_tourType.Size = new System.Drawing.Size(320, 21);
            this.comboBox_tourType.TabIndex = 7;
            // 
            // comboBox_tourDate
            // 
            this.comboBox_tourDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tourDate.FormattingEnabled = true;
            this.comboBox_tourDate.Location = new System.Drawing.Point(103, 120);
            this.comboBox_tourDate.Name = "comboBox_tourDate";
            this.comboBox_tourDate.Size = new System.Drawing.Size(320, 21);
            this.comboBox_tourDate.TabIndex = 9;
            // 
            // label_tourDate
            // 
            this.label_tourDate.AutoSize = true;
            this.label_tourDate.Location = new System.Drawing.Point(15, 123);
            this.label_tourDate.Name = "label_tourDate";
            this.label_tourDate.Size = new System.Drawing.Size(73, 13);
            this.label_tourDate.TabIndex = 8;
            this.label_tourDate.Text = "Дата выхода";
            // 
            // AddInstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 177);
            this.Controls.Add(this.comboBox_tourDate);
            this.Controls.Add(this.label_tourDate);
            this.Controls.Add(this.comboBox_tourType);
            this.Controls.Add(this.label_tourType);
            this.Controls.Add(this.textBox_patronymic);
            this.Controls.Add(this.label_patronymic);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_forename);
            this.Controls.Add(this.textBox_surname);
            this.Controls.Add(this.label_forename);
            this.Controls.Add(this.label_surname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddInstructor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление инструктора";
            this.Load += new System.EventHandler(this.AddInstructor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.TextBox textBox_forename;
        private System.Windows.Forms.TextBox textBox_surname;
        private System.Windows.Forms.Label label_forename;
        private System.Windows.Forms.Label label_surname;
        private System.Windows.Forms.TextBox textBox_patronymic;
        private System.Windows.Forms.Label label_patronymic;
        private System.Windows.Forms.Label label_tourType;
        private System.Windows.Forms.ComboBox comboBox_tourType;
        private System.Windows.Forms.ComboBox comboBox_tourDate;
        private System.Windows.Forms.Label label_tourDate;
    }
}