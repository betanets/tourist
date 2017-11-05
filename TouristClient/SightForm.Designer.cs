namespace TouristClient
{
    partial class SightForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView_sight = new System.Windows.Forms.DataGridView();
            this.button_save = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_sight)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_sight
            // 
            this.dataGridView_sight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_sight.Location = new System.Drawing.Point(13, 13);
            this.dataGridView_sight.Name = "dataGridView_sight";
            this.dataGridView_sight.Size = new System.Drawing.Size(746, 260);
            this.dataGridView_sight.TabIndex = 0;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(623, 283);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(136, 23);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Сохранить изменения";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // SightForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 318);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.dataGridView_sight);
            this.Name = "SightForm";
            this.Text = "Редактирование достопримечательностей";
            this.Load += new System.EventHandler(this.SightForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_sight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_sight;
        private System.Windows.Forms.Button button_save;
    }
}

