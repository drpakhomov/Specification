namespace Specification
{
    partial class specForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(specForm));
            this.doneButton = new System.Windows.Forms.Button();
            this.specNameLabel = new System.Windows.Forms.Label();
            this.specName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(47, 65);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 32);
            this.doneButton.TabIndex = 0;
            this.doneButton.Text = "Готово";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // specNameLabel
            // 
            this.specNameLabel.AutoSize = true;
            this.specNameLabel.Location = new System.Drawing.Point(15, 13);
            this.specNameLabel.Name = "specNameLabel";
            this.specNameLabel.Size = new System.Drawing.Size(140, 17);
            this.specNameLabel.TabIndex = 1;
            this.specNameLabel.Text = "Имя спецификации:";
            // 
            // specName
            // 
            this.specName.Location = new System.Drawing.Point(16, 37);
            this.specName.Name = "specName";
            this.specName.Size = new System.Drawing.Size(137, 22);
            this.specName.TabIndex = 2;
            // 
            // specForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(168, 109);
            this.Controls.Add(this.specName);
            this.Controls.Add(this.specNameLabel);
            this.Controls.Add(this.doneButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "specForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор спецификаций";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Label specNameLabel;
        private System.Windows.Forms.TextBox specName;
    }
}