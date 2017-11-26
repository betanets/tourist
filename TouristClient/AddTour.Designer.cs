namespace TouristClient
{
    partial class AddTour
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
            this.comboBox_tourDate = new System.Windows.Forms.ComboBox();
            this.label_tourDate = new System.Windows.Forms.Label();
            this.comboBox_tourType = new System.Windows.Forms.ComboBox();
            this.label_tourType = new System.Windows.Forms.Label();
            this.textBox_description = new System.Windows.Forms.TextBox();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_forename = new System.Windows.Forms.Label();
            this.label_surname = new System.Windows.Forms.Label();
            this.comboBox_sight = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_tourDate
            // 
            this.comboBox_tourDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tourDate.FormattingEnabled = true;
            this.comboBox_tourDate.Location = new System.Drawing.Point(130, 121);
            this.comboBox_tourDate.Name = "comboBox_tourDate";
            this.comboBox_tourDate.Size = new System.Drawing.Size(320, 21);
            this.comboBox_tourDate.TabIndex = 9;
            // 
            // label_tourDate
            // 
            this.label_tourDate.AutoSize = true;
            this.label_tourDate.Location = new System.Drawing.Point(16, 124);
            this.label_tourDate.Name = "label_tourDate";
            this.label_tourDate.Size = new System.Drawing.Size(73, 13);
            this.label_tourDate.TabIndex = 8;
            this.label_tourDate.Text = "Дата выхода";
            // 
            // comboBox_tourType
            // 
            this.comboBox_tourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_tourType.FormattingEnabled = true;
            this.comboBox_tourType.Location = new System.Drawing.Point(130, 94);
            this.comboBox_tourType.Name = "comboBox_tourType";
            this.comboBox_tourType.Size = new System.Drawing.Size(320, 21);
            this.comboBox_tourType.TabIndex = 7;
            // 
            // label_tourType
            // 
            this.label_tourType.AutoSize = true;
            this.label_tourType.Location = new System.Drawing.Point(16, 97);
            this.label_tourType.Name = "label_tourType";
            this.label_tourType.Size = new System.Drawing.Size(51, 13);
            this.label_tourType.TabIndex = 6;
            this.label_tourType.Text = "Тип тура";
            // 
            // textBox_description
            // 
            this.textBox_description.Location = new System.Drawing.Point(130, 41);
            this.textBox_description.Name = "textBox_description";
            this.textBox_description.Size = new System.Drawing.Size(321, 20);
            this.textBox_description.TabIndex = 3;
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(130, 15);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(321, 20);
            this.textBox_name.TabIndex = 1;
            // 
            // label_forename
            // 
            this.label_forename.AutoSize = true;
            this.label_forename.Location = new System.Drawing.Point(16, 41);
            this.label_forename.Name = "label_forename";
            this.label_forename.Size = new System.Drawing.Size(57, 13);
            this.label_forename.TabIndex = 2;
            this.label_forename.Text = "Описание";
            // 
            // label_surname
            // 
            this.label_surname.AutoSize = true;
            this.label_surname.Location = new System.Drawing.Point(16, 15);
            this.label_surname.Name = "label_surname";
            this.label_surname.Size = new System.Drawing.Size(83, 13);
            this.label_surname.TabIndex = 0;
            this.label_surname.Text = "Наименование";
            // 
            // comboBox_sight
            // 
            this.comboBox_sight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sight.FormattingEnabled = true;
            this.comboBox_sight.Location = new System.Drawing.Point(130, 67);
            this.comboBox_sight.Name = "comboBox_sight";
            this.comboBox_sight.Size = new System.Drawing.Size(320, 21);
            this.comboBox_sight.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Главная достоприм.";
            // 
            // button_cancel
            // 
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(375, 148);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 11;
            this.button_cancel.Text = "Отмена";
            this.button_cancel.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(294, 148);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 10;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // AddTour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 181);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.comboBox_sight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_tourDate);
            this.Controls.Add(this.label_tourDate);
            this.Controls.Add(this.comboBox_tourType);
            this.Controls.Add(this.label_tourType);
            this.Controls.Add(this.textBox_description);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label_forename);
            this.Controls.Add(this.label_surname);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddTour";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление тура";
            this.Load += new System.EventHandler(this.AddTour_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_tourDate;
        private System.Windows.Forms.Label label_tourDate;
        private System.Windows.Forms.ComboBox comboBox_tourType;
        private System.Windows.Forms.Label label_tourType;
        private System.Windows.Forms.TextBox textBox_description;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_forename;
        private System.Windows.Forms.Label label_surname;
        private System.Windows.Forms.ComboBox comboBox_sight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
    }
}