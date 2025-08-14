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
            lblMessage = new Label();
            btnDismiss = new Button();
            SuspendLayout();
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(216, 77);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(50, 20);
            lblMessage.TabIndex = 0;
            lblMessage.Text = "label1";
            lblMessage.Click += lblMessage_Click;
            // 
            // btnDismiss
            // 
            btnDismiss.Location = new Point(183, 137);
            btnDismiss.Name = "btnDismiss";
            btnDismiss.Size = new Size(117, 38);
            btnDismiss.TabIndex = 1;
            btnDismiss.Text = "Dismiss";
            btnDismiss.UseVisualStyleBackColor = true;
            btnDismiss.Click += btnDismiss_Click;
            // 
            // DismissAlarmForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(519, 224);
            Controls.Add(btnDismiss);
            Controls.Add(lblMessage);
            Name = "DismissAlarmForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DismissAlarmForm";
            Load += DismissAlarmForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMessage;
        private Button btnDismiss;
    }
}