namespace Specification
{
    partial class groupForm
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
            this.setName = new System.Windows.Forms.TextBox();
            this.setNameLabel = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // setName
            // 
            this.setName.Location = new System.Drawing.Point(13, 33);
            this.setName.Name = "setName";
            this.setName.Size = new System.Drawing.Size(137, 22);
            this.setName.TabIndex = 5;
            // 
            // setNameLabel
            // 
            this.setNameLabel.AutoSize = true;
            this.setNameLabel.Location = new System.Drawing.Point(9, 9);
            this.setNameLabel.Name = "setNameLabel";
            this.setNameLabel.Size = new System.Drawing.Size(149, 17);
            this.setNameLabel.TabIndex = 4;
            this.setNameLabel.Text = "Имя компл. единицы:";
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(44, 61);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 32);
            this.doneButton.TabIndex = 3;
            this.doneButton.Text = "Готово";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // groupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(165, 103);
            this.Controls.Add(this.setName);
            this.Controls.Add(this.setNameLabel);
            this.Controls.Add(this.doneButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "groupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактор комплектных единиц";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox setName;
        private System.Windows.Forms.Label setNameLabel;
        private System.Windows.Forms.Button doneButton;
    }
}