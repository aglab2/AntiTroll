namespace AntiTroll
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Detroll = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.starRotateCheckBox = new System.Windows.Forms.CheckBox();
            this.showSecretsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 531);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Detroll";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.Location = new System.Drawing.Point(12, 392);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(412, 133);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 531);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Load ROM";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detroll,
            this.ItemNumber,
            this.ItemName,
            this.Parameter});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(412, 374);
            this.dataGridView1.TabIndex = 3;
            // 
            // Detroll
            // 
            this.Detroll.HeaderText = "Detroll";
            this.Detroll.Name = "Detroll";
            this.Detroll.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Detroll.Width = 50;
            // 
            // ItemNumber
            // 
            this.ItemNumber.HeaderText = "#";
            this.ItemNumber.Name = "ItemNumber";
            this.ItemNumber.Width = 50;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Object Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 180;
            // 
            // Parameter
            // 
            this.Parameter.HeaderText = "Parameter";
            this.Parameter.Name = "Parameter";
            this.Parameter.ReadOnly = true;
            this.Parameter.Width = 70;
            // 
            // starRotateCheckBox
            // 
            this.starRotateCheckBox.AutoSize = true;
            this.starRotateCheckBox.Checked = true;
            this.starRotateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.starRotateCheckBox.Location = new System.Drawing.Point(258, 535);
            this.starRotateCheckBox.Name = "starRotateCheckBox";
            this.starRotateCheckBox.Size = new System.Drawing.Size(85, 17);
            this.starRotateCheckBox.TabIndex = 4;
            this.starRotateCheckBox.Text = "Rotate Stars";
            this.starRotateCheckBox.UseVisualStyleBackColor = true;
            // 
            // showSecretsCheckBox
            // 
            this.showSecretsCheckBox.AutoSize = true;
            this.showSecretsCheckBox.Checked = true;
            this.showSecretsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showSecretsCheckBox.Location = new System.Drawing.Point(167, 535);
            this.showSecretsCheckBox.Name = "showSecretsCheckBox";
            this.showSecretsCheckBox.Size = new System.Drawing.Size(90, 17);
            this.showSecretsCheckBox.TabIndex = 5;
            this.showSecretsCheckBox.Text = "Show secrets";
            this.showSecretsCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 566);
            this.Controls.Add(this.showSecretsCheckBox);
            this.Controls.Add(this.starRotateCheckBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Detroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter;
        private System.Windows.Forms.CheckBox starRotateCheckBox;
        private System.Windows.Forms.CheckBox showSecretsCheckBox;
    }
}

