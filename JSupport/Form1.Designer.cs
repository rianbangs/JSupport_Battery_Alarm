namespace JSupport
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblThreshold = new Label();
            numThreshold = new NumericUpDown();
            lblAudio = new Label();
            txtAudioFile = new TextBox();
            btnBrowse = new Button();
            lblMessage = new Label();
            txtMessage = new TextBox();
            batteryCheckTimer = new System.Windows.Forms.Timer(components);
            openFileDialog1 = new OpenFileDialog();
            notifyIcon1 = new NotifyIcon(components);
            trayMenu = new ContextMenuStrip(components);
            menuShow = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            btnSaveProfile = new Button();
            txtProfileName = new TextBox();
            dgvProfiles = new DataGridView();
            ProfileName = new DataGridViewTextBoxColumn();
            BattPercent = new DataGridViewTextBoxColumn();
            Message = new DataGridViewTextBoxColumn();
            Edit = new DataGridViewButtonColumn();
            Delete = new DataGridViewButtonColumn();
            panelProfileDetails = new GroupBox();
            btnUpdateProfile = new Button();
            btnCancelEdit = new Button();
            label1 = new Label();
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            btnClearProfiles = new Button();
            batteryPercentageChecker = new System.Windows.Forms.Timer(components);
            BatteryPercentage = new Label();
            panel1 = new Panel();
            axWindowsMediaPlayer2 = new AxWMPLib.AxWindowsMediaPlayer();
            panel2 = new Panel();
            minimizeBtn = new Button();
            ((System.ComponentModel.ISupportInitialize)numThreshold).BeginInit();
            trayMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvProfiles).BeginInit();
            panelProfileDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer2).BeginInit();
            SuspendLayout();
            // 
            // lblThreshold
            // 
            lblThreshold.AutoSize = true;
            lblThreshold.Location = new Point(-1, 44);
            lblThreshold.Name = "lblThreshold";
            lblThreshold.Size = new Size(151, 20);
            lblThreshold.TabIndex = 0;
            lblThreshold.Text = "Battery Alarm At (%)\"";
            // 
            // numThreshold
            // 
            numThreshold.Location = new Point(172, 44);
            numThreshold.Name = "numThreshold";
            numThreshold.Size = new Size(150, 27);
            numThreshold.TabIndex = 1;
            // 
            // lblAudio
            // 
            lblAudio.AutoSize = true;
            lblAudio.Location = new Point(0, 78);
            lblAudio.Name = "lblAudio";
            lblAudio.Size = new Size(122, 20);
            lblAudio.TabIndex = 2;
            lblAudio.Text = "Alarm Sound File";
            // 
            // txtAudioFile
            // 
            txtAudioFile.Location = new Point(170, 78);
            txtAudioFile.Name = "txtAudioFile";
            txtAudioFile.Size = new Size(150, 27);
            txtAudioFile.TabIndex = 3;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(335, 77);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(94, 29);
            btnBrowse.TabIndex = 4;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(5, 117);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(165, 20);
            lblMessage.TabIndex = 5;
            lblMessage.Text = "Custom Alarm Message";
            // 
            // txtMessage
            // 
            txtMessage.Location = new Point(182, 116);
            txtMessage.Name = "txtMessage";
            txtMessage.Size = new Size(270, 27);
            txtMessage.TabIndex = 6;
            // 
            // batteryCheckTimer
            // 
            batteryCheckTimer.Interval = 1000;
            batteryCheckTimer.Tick += batteryCheckTimer_Tick;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "JSupport Battery Monitor";
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // trayMenu
            // 
            trayMenu.ImageScalingSize = new Size(20, 20);
            trayMenu.Items.AddRange(new ToolStripItem[] { menuShow, menuExit });
            trayMenu.Name = "trayMenu";
            trayMenu.Size = new Size(115, 52);
            trayMenu.Opening += trayMenu_Opening;
            // 
            // menuShow
            // 
            menuShow.Name = "menuShow";
            menuShow.Size = new Size(114, 24);
            menuShow.Text = "Show";
            menuShow.Click += showToolStripMenuItem1_Click;
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(114, 24);
            menuExit.Text = "Exit";
            menuExit.Click += menuExit_Click;
            // 
            // btnSaveProfile
            // 
            btnSaveProfile.Location = new Point(860, 279);
            btnSaveProfile.Name = "btnSaveProfile";
            btnSaveProfile.Size = new Size(115, 29);
            btnSaveProfile.TabIndex = 10;
            btnSaveProfile.Text = "Add Profile";
            btnSaveProfile.UseVisualStyleBackColor = true;
            btnSaveProfile.Click += btnSaveProfile_Click;
            // 
            // txtProfileName
            // 
            txtProfileName.Location = new Point(175, 6);
            txtProfileName.Name = "txtProfileName";
            txtProfileName.Size = new Size(277, 27);
            txtProfileName.TabIndex = 13;
            // 
            // dgvProfiles
            // 
            dgvProfiles.BackgroundColor = SystemColors.ActiveCaptionText;
            dgvProfiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProfiles.Columns.AddRange(new DataGridViewColumn[] { ProfileName, BattPercent, Message, Edit, Delete });
            dgvProfiles.Location = new Point(127, 97);
            dgvProfiles.Name = "dgvProfiles";
            dgvProfiles.RowHeadersWidth = 51;
            dgvProfiles.Size = new Size(848, 168);
            dgvProfiles.TabIndex = 14;
            dgvProfiles.CellContentClick += dgvProfiles_CellContentClick;
            // 
            // ProfileName
            // 
            ProfileName.HeaderText = "Profile Name";
            ProfileName.MinimumWidth = 6;
            ProfileName.Name = "ProfileName";
            ProfileName.Width = 150;
            // 
            // BattPercent
            // 
            BattPercent.HeaderText = "Battery %";
            BattPercent.MinimumWidth = 6;
            BattPercent.Name = "BattPercent";
            BattPercent.Resizable = DataGridViewTriState.True;
            BattPercent.SortMode = DataGridViewColumnSortMode.NotSortable;
            BattPercent.Width = 50;
            // 
            // Message
            // 
            Message.HeaderText = "Message";
            Message.MinimumWidth = 6;
            Message.Name = "Message";
            Message.Width = 180;
            // 
            // Edit
            // 
            Edit.HeaderText = "Edit";
            Edit.MinimumWidth = 6;
            Edit.Name = "Edit";
            Edit.Width = 125;
            // 
            // Delete
            // 
            Delete.HeaderText = "Delete";
            Delete.MinimumWidth = 6;
            Delete.Name = "Delete";
            Delete.Width = 125;
            // 
            // panelProfileDetails
            // 
            panelProfileDetails.Controls.Add(btnUpdateProfile);
            panelProfileDetails.Controls.Add(btnCancelEdit);
            panelProfileDetails.Controls.Add(label1);
            panelProfileDetails.Controls.Add(txtProfileName);
            panelProfileDetails.Controls.Add(numThreshold);
            panelProfileDetails.Controls.Add(lblThreshold);
            panelProfileDetails.Controls.Add(btnBrowse);
            panelProfileDetails.Controls.Add(lblAudio);
            panelProfileDetails.Controls.Add(txtAudioFile);
            panelProfileDetails.Controls.Add(txtMessage);
            panelProfileDetails.Controls.Add(lblMessage);
            panelProfileDetails.Location = new Point(485, 314);
            panelProfileDetails.Name = "panelProfileDetails";
            panelProfileDetails.Size = new Size(490, 200);
            panelProfileDetails.TabIndex = 15;
            panelProfileDetails.TabStop = false;
            // 
            // btnUpdateProfile
            // 
            btnUpdateProfile.Location = new Point(116, 160);
            btnUpdateProfile.Name = "btnUpdateProfile";
            btnUpdateProfile.Size = new Size(94, 29);
            btnUpdateProfile.TabIndex = 16;
            btnUpdateProfile.Text = "save";
            btnUpdateProfile.UseVisualStyleBackColor = true;
            btnUpdateProfile.Click += btnUpdateProfile_Click;
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.Location = new Point(265, 160);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Size = new Size(94, 29);
            btnCancelEdit.TabIndex = 15;
            btnCancelEdit.Text = "Cancel";
            btnCancelEdit.UseVisualStyleBackColor = true;
            btnCancelEdit.Click += btnCancelEdit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 13);
            label1.Name = "label1";
            label1.Size = new Size(96, 20);
            label1.TabIndex = 14;
            label1.Text = "Profile Name";
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(-1, -1);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(1073, 631);
            axWindowsMediaPlayer1.TabIndex = 21;
            axWindowsMediaPlayer1.Enter += axWindowsMediaPlayer1_Enter;
            // 
            // btnClearProfiles
            // 
            btnClearProfiles.Location = new Point(117, 24);
            btnClearProfiles.Name = "btnClearProfiles";
            btnClearProfiles.Size = new Size(134, 29);
            btnClearProfiles.TabIndex = 18;
            btnClearProfiles.Text = "Clear Profiles";
            btnClearProfiles.UseVisualStyleBackColor = true;
            btnClearProfiles.Visible = false;
            btnClearProfiles.Click += btnClearProfiles_Click;
            // 
            // batteryPercentageChecker
            // 
            batteryPercentageChecker.Tick += batteryPercentageChecker_Tick;
            // 
            // BatteryPercentage
            // 
            BatteryPercentage.AutoSize = true;
            BatteryPercentage.BackColor = SystemColors.Desktop;
            BatteryPercentage.Font = new Font("Trebuchet MS", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BatteryPercentage.ForeColor = Color.Transparent;
            BatteryPercentage.Location = new Point(49, 75);
            BatteryPercentage.Name = "BatteryPercentage";
            BatteryPercentage.Size = new Size(52, 20);
            BatteryPercentage.TabIndex = 19;
            BatteryPercentage.Text = "label2";
            // 
            // panel1
            // 
            panel1.Controls.Add(BatteryPercentage);
            panel1.Controls.Add(axWindowsMediaPlayer2);
            panel1.Location = new Point(309, 271);
            panel1.Name = "panel1";
            panel1.Size = new Size(152, 243);
            panel1.TabIndex = 22;
            // 
            // axWindowsMediaPlayer2
            // 
            axWindowsMediaPlayer2.Enabled = true;
            axWindowsMediaPlayer2.Location = new Point(0, 41);
            axWindowsMediaPlayer2.Name = "axWindowsMediaPlayer2";
            axWindowsMediaPlayer2.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer2.OcxState");
            axWindowsMediaPlayer2.Size = new Size(160, 213);
            axWindowsMediaPlayer2.TabIndex = 0;
            axWindowsMediaPlayer2.Enter += axWindowsMediaPlayer2_Enter_1;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImage = Properties.Resources.movable;
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Location = new Point(-1, -1);
            panel2.Name = "panel2";
            panel2.Size = new Size(63, 55);
            panel2.TabIndex = 23;
            panel2.MouseDown += mouse_Down;
            panel2.MouseMove += mouse_Move;
            // 
            // minimizeBtn
            // 
            minimizeBtn.BackColor = Color.Transparent;
            minimizeBtn.BackgroundImage = Properties.Resources.minimize1;
            minimizeBtn.BackgroundImageLayout = ImageLayout.Stretch;
            minimizeBtn.Location = new Point(1007, -4);
            minimizeBtn.Name = "minimizeBtn";
            minimizeBtn.Size = new Size(65, 58);
            minimizeBtn.TabIndex = 25;
            minimizeBtn.UseVisualStyleBackColor = false;
            minimizeBtn.Click += minimizeBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1072, 622);
            Controls.Add(minimizeBtn);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(panelProfileDetails);
            Controls.Add(dgvProfiles);
            Controls.Add(btnSaveProfile);
            Controls.Add(axWindowsMediaPlayer1);
            Controls.Add(btnClearProfiles);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            Resize += Form1_Resize_1;
            ((System.ComponentModel.ISupportInitialize)numThreshold).EndInit();
            trayMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvProfiles).EndInit();
            panelProfileDetails.ResumeLayout(false);
            panelProfileDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblThreshold;
        private NumericUpDown numThreshold;
        private Label lblAudio;
        private TextBox txtAudioFile;
        private Button btnBrowse;
        private Label lblMessage;
        private TextBox txtMessage;
        private System.Windows.Forms.Timer batteryCheckTimer;
        private OpenFileDialog openFileDialog1;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip trayMenu;
        private ToolStripMenuItem menuShow;
        private ToolStripMenuItem menuExit;
        private Button btnSaveProfile;
        private TextBox txtProfileName;
        private DataGridView dgvProfiles;
        private GroupBox panelProfileDetails;
        private Label label1;
        private Button btnCancelEdit;
        private Button btnUpdateProfile;
        private Button btnClearProfiles;
        private System.Windows.Forms.Timer batteryPercentageChecker;
        private Label BatteryPercentage;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private Panel panel1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer2;
        private DataGridViewTextBoxColumn ProfileName;
        private DataGridViewTextBoxColumn BattPercent;
        private DataGridViewTextBoxColumn Message;
        private DataGridViewButtonColumn Edit;
        private DataGridViewButtonColumn Delete;
        private Panel panel2;
        private Button minimizeBtn;
    }
}
