namespace JSupport
{
    partial class DismissAlarmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DismissAlarmForm));
            lblMessage = new Label();
            btnDismiss = new Button();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(97, 409);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(50, 20);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "label1";
            lblMessage.Click += lblMessage_Click;
            // 
            // btnDismiss
            // 
            btnDismiss.Location = new Point(107, 480);
            btnDismiss.Name = "btnDismiss";
            btnDismiss.Size = new Size(117, 38);
            btnDismiss.TabIndex = 1;
            btnDismiss.Text = "Dismiss";
            btnDismiss.UseVisualStyleBackColor = true;
            btnDismiss.Click += btnDismiss_Click;
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(0, 0);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(347, 595);
            axWindowsMediaPlayer1.TabIndex = 2;
            // 
            // DismissAlarmForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(347, 595);
            Controls.Add(btnDismiss);
            Controls.Add(lblMessage);
            Controls.Add(axWindowsMediaPlayer1);
            Name = "DismissAlarmForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DismissAlarmForm";
            Load += DismissAlarmForm_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMessage;
        private Button btnDismiss;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
    }
}