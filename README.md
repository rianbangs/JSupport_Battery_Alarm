# 💻 JSupport

**JSupport - Battery Alarm** is a lightweight Windows desktop utility designed to help users monitor their battery status and trigger custom alarms based on battery percentage levels.

This app is ideal for users who want to:
- Get notified when their battery level drops below or rises above a specified threshold.
- Use personalized messages and custom audio files as alarm sounds.
- Have full control over multiple alarm profiles.
- Run silently in the background with system tray support.

---

## ⚙️ Features

- 🔋 **Battery Monitoring**  
  Automatically checks your battery percentage and power status.

- 📢 **Custom Alarms**  
  Create multiple profiles with:
  - Target battery level
  - Custom alarm sounds (any format)
  - Custom alert messages

- 🎥 **Background Video Support**  
  Uses video files (e.g., `.mp4`) as animated form backgrounds.

- 📌 **Minimize to Tray**  
  Runs silently in the background and shows a balloon tip notification once minimized.

- 🖼️ **Custom Tray Icon**  
  Displays a clean icon in the system tray for easy access.

- 🔁 **Autostart**  
  Automatically adds itself to startup on first run.

---

## 🚀 Installation

- Clone or download the project
- Open the solution in **Visual Studio**
- Build the project using the `.NET 8.0 (Windows)` target

> Optional:
> - Replace the tray icon by setting your own `.ico` file in project properties
> - Configure form design, background video, and custom messages in the designer or code

---

## 📥 How to Download and Use

To use **JSupport - Battery Alarm** without building it yourself:

1. **Download the folder**:  
  [ `JSupport/bin/Debug/net8.0-windows`](https://github.com/rianbangs/JSupport_Battery_Alarm/blob/e695cfeaa0d13563df36fd1f203d27bd812d626b/JSupport/bin/Debug/net8.0-windows.zip)

2. **Run the app**:  
   Double-click `JSupport.exe` to start it.

> ✅ No installation required. The app runs directly from the folder.

---

## 📌 Notes

- To support background videos, make sure the required `.mp4` files are in the `assets` folder.
- Tray balloon notifications show only once after minimizing.
- Alarm triggers work even when minimized to tray.

---

## 📂 Folder Structure
JSupport/
├── assets/
│ └── massive_rasengan.mp4
├── bin/
│ └── Debug/
│ └── net8.0-windows/
│ └── JSupport.exe
├── Properties/
├── Form1.cs
├── DismissAlarmForm.cs
├── Program.cs
└── README.md




---

## 🧠 Author

**Joseph Rian B. Cirunay**  
💼 System Analyst & Programmer

---

## 📃 License

This project is released under the MIT License.

