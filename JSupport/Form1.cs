namespace JSupport
{
    using AxWMPLib;
    using Microsoft.Win32;
    using NAudio.Wave;
    using System;
    using System.Text.Json;
    using System.Windows.Forms;



    public partial class Form1 : Form
    {
        // ✅ Declare your fields here (at class level)
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        private bool alarmTriggered = false;
        private UserSettings settings = new();
        private Dictionary<string, (WaveOutEvent, AudioFileReader)> runningAlarms = new();
        private bool batteryMonitoringPaused = false;
        private int lastBatteryLevel = 0;
        private string profileBeingEdited = null;




        public Point mouseLocation;


        public Form1()
        {
            InitializeComponent();


            this.FormClosing += Form1_FormClosing;

            SetupDataGridView();


            btnCancelEdit.Click += btnCancelEdit_Click;
            this.Load += Form1_Load;



            // Minimize to tray on startup
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = true;


        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }




        private void SetupDataGridView()
        {
            dgvProfiles.Columns.Clear();

            dgvProfiles.Columns.Add("ProfileName", "Profile Name");
            dgvProfiles.Columns.Add("BattPercent", "Battery %");
            dgvProfiles.Columns.Add("Message", "Alarm Message");

            var editBtn = new DataGridViewButtonColumn();
            editBtn.Name = "Edit";
            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            dgvProfiles.Columns.Add(editBtn);

            var deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.Name = "Delete";
            deleteBtn.HeaderText = "Delete";
            deleteBtn.Text = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;
            dgvProfiles.Columns.Add(deleteBtn);

            dgvProfiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }



        private void Form1_Load(object sender, EventArgs e)
        {






            LoadSettings();          // Load saved profiles from file
            SetAutoStart(true);      // Setup for Windows autostart

            alarmTriggered = false;
            batteryCheckTimer.Start();

            batteryPercentageChecker.Start(); // Start battery percentage checker



            //tray icon setup
            trayMenu = new ContextMenuStrip();

            // Add "Show App"
            trayMenu.Items.Add("Show App", null, (s, ea) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            });


            // Add "Exit"
            trayMenu.Items.Add("Exit", null, (s, ea) =>
            {
                Application.Exit();
            });

            notifyIcon1.ContextMenuStrip = trayMenu;


            this.FormBorderStyle = FormBorderStyle.None; // Hide form borders
            this.TopMost = true; // Stay on top






            axWindowsMediaPlayer1.URL = "assets\\battery alarm background.mp4";
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.Dock = DockStyle.Fill;
            axWindowsMediaPlayer1.SendToBack();





        }


        private void StartAlarmsForAllProfiles()
        {
            int batteryPercent = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);

            foreach (var kvp in settings.Profiles)
            {
                string profileName = kvp.Key;
                AlarmProfile profile = kvp.Value;

                if (batteryPercent <= profile.BatteryThreshold && !runningAlarms.ContainsKey(profileName))
                {
                    StartAlarmForProfile(profile);

                }
            }
        }



        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Audio Files|*.mp3;*.wav;*.wma;*.aac;*.flac|All Files|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtAudioFile.Text = openFileDialog1.FileName;
            }
            //using (OpenFileDialog ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "Audio Files|*.mp3;*.wav;*.wma;*.aac;*.flac|All Files|*.*";
            //    if (ofd.ShowDialog() == DialogResult.OK)
            //    {
            //        txtAudioFile.Text = ofd.FileName;
            //    }
            //}
        }






        private void batteryCheckTimer_Tick(object sender, EventArgs e)
        {
            int currentBattery = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            bool isCharging = SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online;

            System.Diagnostics.Debug.WriteLine("************ batteryCheckTimer_Tick  is running ************");


            //if (batteryMonitoringPaused)
            //{
            //    // Resume monitoring if battery is charged above ALL thresholds
            //    bool resume = settings.Profiles.Values.All(profile => currentBattery > profile.BatteryThreshold);

            //    if (currentBattery > lastBatteryLevel)
            //    {
            //        lastBatteryLevel = currentBattery;
            //        batteryMonitoringPaused = false;
            //        batteryCheckTimer.Start(); // Optional, only if timer is stopped elsewhere
            //    }
            //    else
            //    {
            //        return; // Still paused
            //    }
            //}

            // Stop alarms for profiles whose battery is now above threshold
            var profilesToStop = runningAlarms
                .Where(kvp => currentBattery != settings.Profiles[kvp.Key].BatteryThreshold)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var profileName in profilesToStop)
            {
                StopAlarm(profileName);
            }

            // Start alarms for profiles below threshold, not charging
            //if (!isCharging)
            //{
            foreach (var profile in settings.Profiles.Values)
            {
                if (currentBattery <= profile.BatteryThreshold && !runningAlarms.ContainsKey(profile.ProfileName))
                {
                    StartAlarmForProfile(profile);
                }
            }
            //}
        }





        public void StopAlarm(string profileName)
        {
            if (runningAlarms.TryGetValue(profileName, out var tuple))
            {
                tuple.Item1.Stop();
                tuple.Item1.Dispose();
                tuple.Item2.Dispose();
                runningAlarms.Remove(profileName);
            }

            // Optional: Pause monitoring if all alarms are cleared
            if (runningAlarms.Count == 0)
            {
                batteryCheckTimer.Stop();
                batteryMonitoringPaused = true;
            }
        }




        private void PlayAlarm()
        {
            try
            {
                string filePath = txtAudioFile.Text;
                if (System.IO.File.Exists(filePath))
                {
                    // Dispose old instances
                    outputDevice?.Dispose();
                    audioFile?.Dispose();

                    outputDevice = new WaveOutEvent();
                    audioFile = new AudioFileReader(filePath);
                    outputDevice.Init(audioFile);
                    outputDevice.Play();


                }

                MessageBox.Show(txtMessage.Text, "Battery Alarm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing alarm: " + ex.Message);
            }
        }




        //private void btnStop_Click(object sender, EventArgs e)
        //{
        //    outputDevice?.Stop();
        //    outputDevice?.Dispose();
        //    audioFile?.Dispose();

        //    outputDevice = null;
        //    audioFile = null;


        //    MessageBox.Show("Alarm stopped.");
        //}


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            outputDevice?.Dispose();
            audioFile?.Dispose();
            base.OnFormClosing(e);
        }


        private readonly string settingsPath = "settings.json";

        private void SaveSettings()
        {
            string profileName = txtProfileName.Text.Trim();
            if (string.IsNullOrWhiteSpace(profileName)) return;

            var profile = new AlarmProfile
            {
                BatteryThreshold = (int)numThreshold.Value,
                AudioFilePath = txtAudioFile.Text,
                CustomMessage = txtMessage.Text
            };

            settings.Profiles[profileName] = profile;
            settings.LastProfile = profileName;

            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(settingsPath, json);

            LoadProfileList();
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsPath))
            {
                try
                {
                    var json = File.ReadAllText(settingsPath);
                    settings = JsonSerializer.Deserialize<UserSettings>(json);
                    LoadProfileList();

                    // Load and select last used profile
                    if (!string.IsNullOrEmpty(settings.LastProfile) && settings.Profiles.ContainsKey(settings.LastProfile))
                    {
                        LoadProfile(settings.LastProfile);
                        txtProfileName.Text = settings.LastProfile;

                        // Select the corresponding row in the DataGridView
                        foreach (DataGridViewRow row in dgvProfiles.Rows)
                        {
                            if (row.Cells["ProfileName"].Value?.ToString() == settings.LastProfile)
                            {
                                row.Selected = true;
                                dgvProfiles.CurrentCell = row.Cells[0]; // Focus first cell
                                break;
                            }
                        }

                        // Show and populate the profile editing panel
                        panelProfileDetails.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    settings = new UserSettings(); // fallback to blank settings
                }
            }
        }




        private void LoadProfile(string profileName)
        {
            if (settings.Profiles.TryGetValue(profileName, out var profile))
            {
                numThreshold.Value = profile.BatteryThreshold;
                txtAudioFile.Text = profile.AudioFilePath;
                txtMessage.Text = profile.CustomMessage;
            }
        }

        private void LoadProfileList()
        {
            dgvProfiles.Rows.Clear();
            foreach (var profile in settings.Profiles.Values)
            {
                dgvProfiles.Rows.Add(
                    profile.ProfileName,
                    profile.BatteryThreshold + "%",
                    profile.CustomMessage
                );

                profileBeingEdited = profile.ProfileName;
            }

        }


        private void btnAddProfile_Click(object sender, EventArgs e)
        {
            ClearProfileDetailsFields();
            ShowProfileDetailsPanel(true);
            txtProfileName.Enabled = true;
        }



        private void trayMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void menuExit_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }

        private void menuShow_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Show();                      // Show the form
            this.WindowState = FormWindowState.Normal;  // Restore if minimized
            this.ShowInTaskbar = true;        // Show in taskbar
            notifyIcon1.Visible = false;
        }




        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();                      // Show the form
            this.WindowState = FormWindowState.Normal;  // Restore if minimized
            this.ShowInTaskbar = true;        // Show in taskbar
            notifyIcon1.Visible = false;      // Hide tray icon
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool trayTipShown = false;
        private void Form1_Resize_1(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;       // Show tray icon
                this.ShowInTaskbar = false;       // Hide from taskbar
                this.Hide();                      // Hide the form window


                if (!trayTipShown)
                {
                    notifyIcon1.BalloonTipTitle = "JSupport Running";
                    notifyIcon1.BalloonTipText = "Monitoring battery in background.";
                    notifyIcon1.ShowBalloonTip(3000);
                    trayTipShown = true; // prevent showing again
                }
            }
            else
            {
                trayTipShown = false; // reset when not minimized
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();                             // Show the form
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void SetAutoStart(bool enable)
        {
            string appName = "JSupport";
            string exePath = Application.ExecutablePath;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (enable)
                {
                    key.SetValue(appName, exePath);
                }
                else
                {
                    key.DeleteValue(appName, false); // Won’t throw if key doesn't exist
                }
            }
        }



        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            string profileName = txtProfileName.Text.Trim();
            if (string.IsNullOrWhiteSpace(profileName))
            {
                MessageBox.Show("Profile name cannot be empty.", "Warning");
                return;
            }

            var profile = new AlarmProfile
            {
                BatteryThreshold = (int)numThreshold.Value,
                AudioFilePath = txtAudioFile.Text,
                CustomMessage = txtMessage.Text,
                ProfileName = profileName  // <-- Set this!

            };


            if (profileBeingEdited == null)
            {
                // New profile: check for duplicates
                if (settings.Profiles.ContainsKey(profileName))
                {
                    MessageBox.Show("Profile name already exists.", "Warning");
                    return;
                }
            }
            else if (profileBeingEdited != profileName)
            {
                // Rename scenario
                if (settings.Profiles.ContainsKey(profileName))
                {
                    MessageBox.Show("Profile name already exists.", "Warning");
                    return;
                }
                settings.Profiles.Remove(profileBeingEdited);
            }

            settings.Profiles[profileName] = profile;
            settings.LastProfile = profileName;

            File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));

            LoadProfileList();

            profileBeingEdited = null;
        }





        private void SaveSettingsToFile()
        {
            var json = JsonSerializer.Serialize(settings);
            File.WriteAllText(settingsPath, json);
        }


        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            ClearProfileForm();
            profileBeingEdited = null;
        }

        private void ClearProfileForm()
        {
            txtProfileName.Text = "";
            numThreshold.Value = 30;
            txtAudioFile.Text = "";
            txtMessage.Text = "";
        }










        private void ShowProfileDetailsPanel(bool show)
        {
            panelProfileDetails.Visible = show;
            dgvProfiles.Enabled = !show;

            if (!show)
            {
                ClearProfileDetailsFields();
                txtProfileName.Enabled = true; // re-enable when adding new profile
            }
        }

        private void ClearProfileDetailsFields()
        {
            txtProfileName.Clear();
            numThreshold.Value = 20; // default threshold
            txtAudioFile.Clear();
            txtMessage.Clear();
        }


        private void dgvProfiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Get clicked column name
            string columnName = dgvProfiles.Columns[e.ColumnIndex].Name;

            // Get the profile name in this row
            string profileName = dgvProfiles.Rows[e.RowIndex].Cells["ProfileName"].Value?.ToString();

            if (columnName == "Delete")
            {
                // Confirm deletion
                var result = MessageBox.Show($"Are you sure you want to delete profile \"{profileName}\"?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (settings.Profiles.ContainsKey(profileName))
                    {
                        settings.Profiles.Remove(profileName);

                        // Clear selection if deleted profile was selected
                        if (settings.LastProfile == profileName)
                            settings.LastProfile = null;

                        File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));

                        LoadProfileList();
                        MessageBox.Show("Profile deleted successfully.", "Deleted");
                    }
                }
            }
            else if (columnName == "Edit")
            {
                if (string.IsNullOrEmpty(profileName)) return;
                profileBeingEdited = profileName;

                // Load the profile into the detail panel for editing
                LoadProfile(profileName);

                // Show the profile details panel and enable edit mode
                panelProfileDetails.Visible = true;

                // Example: enable save and cancel buttons, disable edit button if any
                btnSaveProfile.Enabled = true;
                btnCancelEdit.Visible = true;


                // Populate the profile name textbox (if editable)
                txtProfileName.Text = profileName;


            }
            else if (columnName == "StartAlarm")
            {
                if (settings.Profiles.TryGetValue(profileName, out AlarmProfile profile))
                {
                    StartAlarmForProfile(profile);
                }
            }

        }




        private bool IsBatteryBelowThreshold(int threshold)
        {
            int currentBattery = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            //return currentBattery <= threshold;
            return currentBattery == threshold;
        }




        private void StartAlarmForProfile(AlarmProfile profile)
        {
            string profileName = profile.ProfileName;

            //Prevent starting the same alarm multiple times
            if (runningAlarms.ContainsKey(profileName))
            {
                return;
            }

            if (!IsBatteryBelowThreshold(profile.BatteryThreshold))
            {

                return;
            }


            try
            {
                if (!File.Exists(profile.AudioFilePath))
                {
                    MessageBox.Show("Audio file not found: " + profile.AudioFilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var outputDevice = new WaveOutEvent();
                var audioFile = new AudioFileReader(profile.AudioFilePath);
                outputDevice.Init(audioFile);
                outputDevice.Play();

                runningAlarms[profileName] = (outputDevice, audioFile);

                if (!string.IsNullOrWhiteSpace(profile.CustomMessage))
                {
                    using (var form = new DismissAlarmForm(profile.CustomMessage, this, profile.ProfileName))
                    {
                        var result = form.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            StopAlarm(profileName);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting alarm: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }







        //private void btnEditProfile_Click(object sender, EventArgs e)
        //{
        //    if (dgvProfiles.CurrentRow != null)
        //    {
        //        profileBeingEdited = dgvProfiles.CurrentRow.Cells["ProfileName"].Value.ToString();
        //        LoadProfile(profileBeingEdited);

        //        txtProfileName.Text = profileBeingEdited;
        //        panelProfileDetails.Visible = true;
        //    }
        //}



        private void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(profileBeingEdited))
            {
                MessageBox.Show("No profile is currently being edited.");
                return;
            }

            string newProfileName = txtProfileName.Text.Trim();
            //int threshold = (int)numThreshold.Value;
            //string audioFile = txtAudioFile.Text.Trim();
            //string message = txtMessage.Text.Trim();



            //if (string.IsNullOrEmpty(newProfileName))
            //{
            //    MessageBox.Show("Profile name cannot be empty.");
            //    return;
            //}

            //// If the name was changed
            //bool nameChanged = newProfileName != profileBeingEdited;



            //// If name changed and new name already exists
            //if (nameChanged && settings.Profiles.ContainsKey(newProfileName))
            //{
            //    MessageBox.Show("A profile with that name already exists.");
            //    return;
            //}

            //// Remove old profile if name changed
            //if (nameChanged)
            //{
            settings.Profiles.Remove(profileBeingEdited);
            //if (settings.LastProfile == profileBeingEdited)
            //{
            //    settings.LastProfile = newProfileName;
            //}
            ////}

            ////// Save new or updated profile
            //settings.Profiles[newProfileName] = new AlarmProfile
            //{
            //    BatteryThreshold = threshold,
            //    AudioFilePath = audioFile,
            //    CustomMessage = message
            //};

            //// Save to file
            //File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));

            //// Reload list and clear editor
            //LoadProfileList();
            //profileBeingEdited = null;
            ////panelProfileDetails.Visible = false;
            ///

            string profileName = txtProfileName.Text.Trim();
            if (string.IsNullOrWhiteSpace(profileName))
            {
                MessageBox.Show("Profile name cannot be empty.", "Warning");
                return;
            }

            var profile = new AlarmProfile
            {
                BatteryThreshold = (int)numThreshold.Value,
                AudioFilePath = txtAudioFile.Text,
                CustomMessage = txtMessage.Text,
                ProfileName = profileName  // <-- Set this!

            };


            if (profileBeingEdited == null)
            {
                // New profile: check for duplicates
                if (settings.Profiles.ContainsKey(profileName))
                {
                    MessageBox.Show("Profile name already exists.", "Warning");
                    return;
                }
            }
            else if (profileBeingEdited != profileName)
            {
                // Rename scenario
                if (settings.Profiles.ContainsKey(profileName))
                {
                    MessageBox.Show("Profile name already exists.", "Warning");
                    return;
                }
                settings.Profiles.Remove(profileBeingEdited);
            }

            settings.Profiles[profileName] = profile;
            settings.LastProfile = profileName;

            File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));

            LoadProfileList();

            MessageBox.Show("Profile updated successfully.");
            profileBeingEdited = txtProfileName.Text.Trim();
        }



        private void btnClearProfiles_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure you want to clear all profiles? This cannot be undone.",
                                                "Confirm Clear",
                                                MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                settings.Profiles.Clear();
                settings.LastProfile = null;

                File.WriteAllText(settingsPath, JsonSerializer.Serialize(settings));
                LoadProfileList();



                profileBeingEdited = null;
            }
        }

        private void alarmPausedChecker_Tick(object sender, EventArgs e)
        {
            int currentBattery = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            if (batteryMonitoringPaused)
            {

                if (currentBattery > lastBatteryLevel)
                {
                    lastBatteryLevel = currentBattery;
                    batteryMonitoringPaused = false;
                    batteryCheckTimer.Start(); // Optional, only if timer is stopped elsewhere
                }
                else
                {
                    return; // Still paused
                }
            }
        }

        private PowerLineStatus lastPowerStatus = PowerLineStatus.Unknown;

        private void batteryPercentageChecker_Tick(object sender, EventArgs e)
        {



            BatteryPercentage.Font = new Font(lblMessage.Font.FontFamily, 15);

            int currentBattery = (int)(SystemInformation.PowerStatus.BatteryLifePercent * 100);
            BatteryPercentage.Text = $"{currentBattery}%";

            PowerLineStatus currentStatus = SystemInformation.PowerStatus.PowerLineStatus;

            // Only update video if battery level OR charger status changes
            if (currentBattery != lastBatteryLevel || currentStatus != lastPowerStatus)
            {
                lastBatteryLevel = currentBattery;
                lastPowerStatus = currentStatus;

                batteryMonitoringPaused = false;
                batteryCheckTimer.Start();

                if (currentStatus == PowerLineStatus.Online)
                {
                    axWindowsMediaPlayer2.URL = "assets\\battery1.mp4";
                }
                else
                {
                    axWindowsMediaPlayer2.URL = "assets\\battery not charging.mp4";
                }

                // Set up video player only once per change
                axWindowsMediaPlayer2.uiMode = "none";
                axWindowsMediaPlayer2.settings.setMode("loop", true);
                axWindowsMediaPlayer2.stretchToFit = true;
                axWindowsMediaPlayer2.Dock = DockStyle.Fill;
                axWindowsMediaPlayer2.SendToBack();
            }

            // Debug logs
            System.Diagnostics.Debug.WriteLine($"Battery: {currentBattery}%, Status: {currentStatus}");
            System.Diagnostics.Debug.WriteLine("batteryMonitoringPaused: " + batteryMonitoringPaused);
        }


        private void btnStart_Click(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void axWindowsMediaPlayer2_Enter(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer2_Enter_1(object sender, EventArgs e)
        {

        }

        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mousePose.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            // Minimize to tray on startup
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = true;
        }
    }
}
