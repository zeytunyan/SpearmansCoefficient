namespace Client
{
    partial class Spearman_Form
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
            System.Windows.Forms.Label Number_label;
            this.Number_vars = new System.Windows.Forms.NumericUpDown();
            this.FirstOK = new System.Windows.Forms.Button();
            Number_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Number_vars)).BeginInit();
            this.SuspendLayout();
            // 
            // Number_label
            // 
            Number_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            Number_label.Location = new System.Drawing.Point(12, 24);
            Number_label.Name = "Number_label";
            Number_label.Size = new System.Drawing.Size(293, 27);
            Number_label.TabIndex = 0;
            Number_label.Text = "Введите количество переменных:";
            // 
            // Number_vars
            // 
            this.Number_vars.Location = new System.Drawing.Point(289, 27);
            this.Number_vars.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Number_vars.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Number_vars.Name = "Number_vars";
            this.Number_vars.Size = new System.Drawing.Size(60, 20);
            this.Number_vars.TabIndex = 1;
            this.Number_vars.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // FirstOK
            // 
            this.FirstOK.Location = new System.Drawing.Point(169, 61);
            this.FirstOK.Name = "FirstOK";
            this.FirstOK.Size = new System.Drawing.Size(75, 23);
            this.FirstOK.TabIndex = 2;
            this.FirstOK.Text = "OK";
            this.FirstOK.UseVisualStyleBackColor = true;
            this.FirstOK.Click += new System.EventHandler(this.FirstOk_Click);
            // 
            // Spearman_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 96);
            this.Controls.Add(this.FirstOK);
            this.Controls.Add(this.Number_vars);
            this.Controls.Add(Number_label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Spearman_Form";
            this.Text = "Spearman\'s rank correlation coefficient";
            ((System.ComponentModel.ISupportInitialize)(this.Number_vars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button FirstOK;
        public System.Windows.Forms.NumericUpDown Number_vars;
    }
}

