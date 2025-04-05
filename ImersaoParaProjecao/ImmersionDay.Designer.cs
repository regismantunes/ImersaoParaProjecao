namespace ImmersionToProjection
{
    partial class ImmersionDay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblImersionItems = new Label();
            btnCopyTitleToClipboard = new Button();
            label1 = new Label();
            btnCopyContentToClipboard = new Button();
            lblWeekday = new Label();
            SuspendLayout();
            // 
            // lblImersionItems
            // 
            lblImersionItems.AutoSize = true;
            lblImersionItems.ForeColor = Color.WhiteSmoke;
            lblImersionItems.Location = new Point(3, 29);
            lblImersionItems.Name = "lblImersionItems";
            lblImersionItems.Size = new Size(16, 60);
            lblImersionItems.TabIndex = 1;
            lblImersionItems.Text = "1.\r\n2.\r\n3.\r\n4.";
            // 
            // btnCopyTitleToClipboard
            // 
            btnCopyTitleToClipboard.BackColor = Color.FromArgb(32, 32, 32);
            btnCopyTitleToClipboard.FlatAppearance.BorderSize = 0;
            btnCopyTitleToClipboard.FlatStyle = FlatStyle.Flat;
            btnCopyTitleToClipboard.ForeColor = Color.WhiteSmoke;
            btnCopyTitleToClipboard.Location = new Point(240, 3);
            btnCopyTitleToClipboard.Name = "btnCopyTitleToClipboard";
            btnCopyTitleToClipboard.Size = new Size(68, 23);
            btnCopyTitleToClipboard.TabIndex = 2;
            btnCopyTitleToClipboard.Text = "Título";
            btnCopyTitleToClipboard.UseVisualStyleBackColor = false;
            btnCopyTitleToClipboard.Click += btnCopyTitleToClipboard_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(140, 6);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 3;
            label1.Text = "Copiar para app:";
            // 
            // btnCopyContentToClipboard
            // 
            btnCopyContentToClipboard.BackColor = Color.FromArgb(32, 32, 32);
            btnCopyContentToClipboard.FlatAppearance.BorderSize = 0;
            btnCopyContentToClipboard.FlatStyle = FlatStyle.Flat;
            btnCopyContentToClipboard.ForeColor = Color.WhiteSmoke;
            btnCopyContentToClipboard.Location = new Point(314, 3);
            btnCopyContentToClipboard.Name = "btnCopyContentToClipboard";
            btnCopyContentToClipboard.Size = new Size(68, 23);
            btnCopyContentToClipboard.TabIndex = 4;
            btnCopyContentToClipboard.Text = "Conteúdo";
            btnCopyContentToClipboard.UseVisualStyleBackColor = false;
            btnCopyContentToClipboard.Click += btnCopyContentToClipboard_Click;
            // 
            // lblWeekday
            // 
            lblWeekday.AutoSize = true;
            lblWeekday.ForeColor = Color.WhiteSmoke;
            lblWeekday.Location = new Point(3, 7);
            lblWeekday.Name = "lblWeekday";
            lblWeekday.Size = new Size(55, 15);
            lblWeekday.TabIndex = 5;
            lblWeekday.Text = "Weekday";
            // 
            // ImersionDay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            Controls.Add(lblWeekday);
            Controls.Add(btnCopyContentToClipboard);
            Controls.Add(label1);
            Controls.Add(btnCopyTitleToClipboard);
            Controls.Add(lblImersionItems);
            Name = "ImersionDay";
            Size = new Size(397, 93);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblImersionItems;
        private Button btnCopyTitleToClipboard;
        private Label label1;
        private Button btnCopyContentToClipboard;
        private Label lblWeekday;
    }
}
