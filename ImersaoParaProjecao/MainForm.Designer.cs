namespace ImmersionToProjection
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCopyToClipboard = new Button();
            flpImersionDays = new FlowLayoutPanel();
            lblMessageTitle = new Label();
            SuspendLayout();
            // 
            // btnCopyToClipboard
            // 
            btnCopyToClipboard.BackColor = Color.FromArgb(32, 32, 32);
            btnCopyToClipboard.FlatAppearance.BorderSize = 0;
            btnCopyToClipboard.FlatStyle = FlatStyle.Flat;
            btnCopyToClipboard.ForeColor = Color.WhiteSmoke;
            btnCopyToClipboard.Location = new Point(12, 12);
            btnCopyToClipboard.Name = "btnCopyToClipboard";
            btnCopyToClipboard.Size = new Size(140, 23);
            btnCopyToClipboard.TabIndex = 1;
            btnCopyToClipboard.Text = "Copiar para projeção";
            btnCopyToClipboard.UseVisualStyleBackColor = false;
            btnCopyToClipboard.Click += btnCopyToClipboard_Click;
            // 
            // flpImersionDays
            // 
            flpImersionDays.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flpImersionDays.AutoScroll = true;
            flpImersionDays.Location = new Point(12, 41);
            flpImersionDays.Name = "flpImersionDays";
            flpImersionDays.Size = new Size(834, 567);
            flpImersionDays.TabIndex = 2;
            flpImersionDays.Resize += flpImersionDays_Resize;
            // 
            // lblMessageTitle
            // 
            lblMessageTitle.AutoSize = true;
            lblMessageTitle.ForeColor = Color.WhiteSmoke;
            lblMessageTitle.Location = new Point(158, 16);
            lblMessageTitle.Name = "lblMessageTitle";
            lblMessageTitle.Size = new Size(0, 15);
            lblMessageTitle.TabIndex = 3;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(858, 620);
            Controls.Add(lblMessageTitle);
            Controls.Add(flpImersionDays);
            Controls.Add(btnCopyToClipboard);
            Name = "MainForm";
            Text = "Imersão Para Projeção";
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnCopyToClipboard;
        private FlowLayoutPanel flpImersionDays;
        private Label lblMessageTitle;
    }
}
