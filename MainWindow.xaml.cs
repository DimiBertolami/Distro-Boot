using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QemuUtil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// ToDo : dropdown menu with all distrowatch isos
    /// </summary>

    public partial class MainWindow : Window
    {
        int hWnd = 0;
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        static readonly HttpClient client = new HttpClient();

        private static Timer aTimer;

        public MainWindow()
        {
            
            //aTimer.Elapsed += OnTimedEvent;
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true;



            InitializeComponent();
            Process ps = CreateProcess();
            Global.ps = ps;
            Global.id = ps.Id;
            int hWnd = ps.MainWindowHandle.ToInt32();
            Global.Handle = hWnd;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Process ps = Global.ps;
            //ps.StandardInput.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);


            //string Dlsp = string.Format("{0} kb/s" + (e.BytesReceived / 1024d / Global.stopwatch.Elapsed.TotalSeconds).ToString("0.00"));
            //MessageBox.Show(Dlsp);


        }


        public Process CreateProcess()
        {
            Process ps = new();
            ps.StartInfo.RedirectStandardInput = true;
            ps.StartInfo.CreateNoWindow = false;
            ps.StartInfo.FileName = "cmd.exe";
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.WorkingDirectory = "c:\\ISO";
            ps.Start();
            //ps.StandardInput.WriteLine("echo off & cls");
            //ps.StandardInput.WriteLine(":: cmd pid= " + ps.Id);
            Global.ps = ps;
            Global.id = ps.Id;
            Global.Handle = ps.MainWindowHandle.ToInt32();
            return ps;
        }

        private void BootHidden(object sender, MouseButtonEventArgs e)
        {
            Process ps = Global.ps;
            int hWnd = ps.MainWindowHandle.ToInt32();
            Global.Handle = hWnd;
            ShowWindow(hWnd, SW_HIDE);
            if (!File.Exists("C:\\Program Files\\qemu\\qemu-system-x86_64.exe")) 
            { ps.StandardInput.WriteLine("powershell -command \"winget install qemu --force\""); }
            L_Boot.WindowState = WindowState.Minimized;
            string fileName = IMGs.SelectedValue.ToString();
            if (fileName.Contains(".IMG") || fileName.Contains(".img"))
            { 
                ps.StandardInput.WriteLine("\"C:\\Program Files\\qemu\\qemu-system-x86_64.exe\" -m 10G -drive file=" + 
                (char)34 + fileName + (char)34 + ",format=raw,index=0,media=disk -vga virtio -no-reboot");
                Command.Content = "\"C:\\Program Files\\qemu\\qemu-system-x86_64.exe\" -m 10G -drive file=" +
                (char)34 + fileName + (char)34 + ",format=raw,index=0,media=disk -vga virtio -no-reboot";
            };
            if (fileName.Contains(".ISO") || fileName.Contains(".iso"))
            { 
              ps.StandardInput.WriteLine("\"C:\\Program Files\\qemu\\qemu-system-x86_64.exe\" -cdrom " +
                (char)34 + fileName + (char)34 + " -m 10G");
                Command.Content = "\"C:\\Program Files\\qemu\\qemu-system-x86_64.exe\" -cdrom " +
                (char)34 + fileName + (char)34 + " -m 10G";
            };
            if (ps.HasExited)
            {
                MessageBox.Show("closed!");
                L_Boot.WindowState = WindowState.Normal;
            }
            ShowOutput.IsChecked = false;
        }

        private void FindImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new();
            dlg.InitialDirectory = "c:\\ISO\\";

            dlg.ShowDialog();
            string fileName = dlg.FileName;
            if (!IMGs.Items.Contains(fileName))
            {
                IMGs.Items.Add(fileName);
                Global.ps.StandardInput.WriteLine(":: Added " + fileName);
            }
        }

        private void RemoveImage(object sender, RoutedEventArgs e)
        {
            int Index = IMGs.SelectedIndex;
            Process ps = Global.ps;
            ps.StandardInput.WriteLine(":: Removing " + IMGs.Items.GetItemAt(Index)); //.CurrentItem.ToString());
            IMGs.Items.RemoveAt(Index);
        }

        private void ScanImages(object sender, RoutedEventArgs e)
        {
            IMGs.Items.Clear();
            int Counter = 0;

            foreach (var item in Directory.EnumerateFiles("c:\\ISO", "*"))
            {
                if(item.ToUpper().EndsWith(".ISO") || item.ToUpper().EndsWith(".IMG"))
                {
                    IMGs.Items.Add(item);
                    Process ps = Global.ps;
                }
            }
            //Scanner.IsEnabled = false;
            //Scanner.Visibility = Visibility.Hidden;
        }


        private static void DownloadFileCallback2(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("File download cancelled.");
            }

            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
        }


        private void DownloadImage(object sender, RoutedEventArgs e)
        {
            if (ISOUrls.SelectedIndex.ToString()== "-1")
            {
                MessageBox.Show("please select an item first..");
            } 
            else 
            {
                int positionCombo = ISOUrls.SelectedIndex + 1;
                //MessageBox.Show("position in combolist: " + (positionCombo).ToString());
                Process ps = Global.ps;
                string response = string.Empty;
                int Counter = 0;
                WebClient webClient = new();

                aTimer = new System.Timers.Timer();
                aTimer.Interval = 500;


                //webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                
                //webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);


                switch (positionCombo)
                {
                    case 0:

                        try
                        {
                            // The variable that will be holding the url address (making sure it starts with http://)
                            Uri URL1 = new Uri("https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_x64.iso");


                            // Start downloading the file
                            webClient.DownloadFileAsync(URL1, @"c:\\ISO\\MX-21.3_x64.iso");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        //webClient.DownloadFile(
                        //    "https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_x64.iso",
                        //    @"c:\\ISO\\MX-21.3_x64.iso");
                        IMGs.Items.Add("c:\\ISO\\MX-21.3_x64.iso");
                        break;
                    case 1:
                        webClient.DownloadFile(
                            "https://mirror.alpix.eu/endeavouros/iso/EndeavourOS_Cassini_neo_22_12.iso",
                            @"c:\\ISO\\EndeavourOS_Cassini_neo_22_12.iso");
                        IMGs.Items.Add("c:\\ISO\\EndeavourOS_Cassini_neo_22_12.iso");
                        break;
                    case 2:
                        webClient.DownloadFile(
                            "https://mirror.crexio.com/linuxmint/isos/stable/21.1/linuxmint-21.1-cinnamon-64bit.iso",
                            @"c:\\ISO\\linuxmint-21.1-cinnamon-64bit.iso");
                        IMGs.Items.Add("c:\\ISO\\linuxmint-21.1-cinnamon-64bit.iso");
                        break;
                    case 3:
                        webClient.DownloadFile(
                            "https://download.manjaro.org/kde/22.0.3/manjaro-kde-22.0.3-230213-linux61.iso",
                            @"c:\\ISO\\manjaro-kde-22.0.3-230213-linux61.iso");
                        IMGs.Items.Add("c:\\ISO\\manjaro-kde-22.0.3-230213-linux61.iso");
                        break;
                    case 4:
                        webClient.DownloadFile(
                            "https://download.manjaro.org/gnome/22.0.3/manjaro-gnome-22.0.3-230213-linux61.iso",
                            @"c:\\ISO\\manjaro-gnome-22.0.3-230213-linux61.iso");
                        IMGs.Items.Add("c:\\ISO\\manjaro-gnome-22.0.3-230213-linux61.iso");
                        break;
                    case 5:
                        webClient.DownloadFile(
                            "https://iso.pop-os.org/22.04/amd64/intel/22/pop-os_22.04_amd64_intel_22.iso",
                            @"c:\\ISO\\pop-os_22.04_amd64_intel_22.iso");
                        IMGs.Items.Add("c:\\ISO\\pop-os_22.04_amd64_intel_22.iso");
                        break;
                    case 6:
                        webClient.DownloadFile(
                            "https://iso.pop-os.org/22.04/amd64/nvidia/22/pop-os_22.04_amd64_nvidia_22.iso",
                            @"c:\\ISO\\pop-os_22.04_amd64_nvidia_22.iso");
                        IMGs.Items.Add("c:\\ISO\\pop-os_22.04_amd64_nvidia_22.iso");
                        break;
                    case 7:
                        webClient.DownloadFile(
                            "https://download.fedoraproject.org/pub/fedora/linux/releases/37/Workstation/x86_64/iso/Fedora-Workstation-Live-x86_64-37-1.7.iso",
                            @"c:\\ISO\\Fedora-Workstation-Live-x86_64-37-1.7.iso");
                        IMGs.Items.Add("c:\\ISO\\Fedora workstation live");
                        break;
                    case 8:
                        webClient.DownloadFile(
                            "https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-desktop-amd64.iso",
                            @"c:\\ISO\\ubuntu-22.04.1-desktop-amd64.iso");
                        IMGs.Items.Add("c:\\ISO\\ubuntu-22.04.1-desktop-amd64.iso");
                        break;
                    case 9:
                        webClient.DownloadFile(
                            "https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-live-server-amd64.iso",
                            @"c:\\ISO\\ubuntu-22.04.1-live-server-amd64.iso");
                        IMGs.Items.Add("c:\\ISO\\ubuntu-22.04.1-live-server-amd64.iso");
                        break;
                    case 10:
                        webClient.DownloadFile(
                            "https://cdimage.debian.org/debian-cd/current/amd64/iso-dvd/debian-11.6.0-amd64-DVD-1.iso",
                            @"c:\\ISO\\debian-11.6.0-amd64-DVD-1.iso");
                        IMGs.Items.Add("c:\\ISO\\debian-11.6.0-amd64-DVD-1.iso");
                        break;
                    case 11:
                        webClient.DownloadFile(
                            "https://ftp.halifax.rwth-aachen.de/osdn/storage/g/l/li/linuxlite/6.2/linux-lite-6.2-64bit.iso",
                            @"c:\\ISO\\linux-lite-6.2-64bit.iso");
                        IMGs.Items.Add("c:\\ISO\\Linux-lite-6.2-64bit.iso");
                        break;
                    case 12:
                        webClient.DownloadFile(
                            "https://sourceforge.net/projects/garuda-linux/files/garuda/dr460nized/221019/garuda-dr460nized-linux-zen-221019.iso",
                            @"c:\\ISO\\garuda-dr460nized-linux-zen-221019.iso");
                        IMGs.Items.Add("c:\\ISO\\garuda-dr460nized-linux-zen-221019.iso");
                        break;
                    case 13:
                        webClient.DownloadFile(
                            "https://ams3.dl.elementary.io/download/MTY3Njc4NTg3Nw==/elementaryos-7.0-stable.20230129rc.iso",
                            @"c:\\ISO\\elementaryos-7.0-stable.20230129rc.iso");
                        IMGs.Items.Add("c:\\ISO\\elementaryos-7.0-stable.20230129rc.iso");
                        break;
                    case 14:
                        webClient.DownloadFile(
                            "https://sourceforge.net/projects/voyagerlive/files/latest/download",
                            @"c:\\ISO\\voyagerlive.iso");
                        IMGs.Items.Add("c:\\ISO\\voyagerlive.iso");
                        break;
                    case 15:
                        webClient.DownloadFile(
                            "https://downloads.bandshed.net/AVL-MXE_21.3/AV_Linux_MX_Edition-21.3_ahs_x64.iso",
                            @"c:\\ISO\\AV_Linux_MX_Edition-21.3_ahs_x64.iso");
                        IMGs.Items.Add("c:\\ISO\\AV_Linux_MX_Edition-21.3_ahs_x64.iso");
                        break;
                    case 16:
                        webClient.DownloadFile(
                            "http://mirror.rit.edu/haiku/r1beta4/haiku-r1beta4-x86_64-anyboot.iso",
                            @"c:\\ISO\\haiku-r1beta4-x86_64-anyboot.iso");
                        IMGs.Items.Add("c:\\ISO\\haiku-r1beta4-x86_64-anyboot.iso");
                        break;
                    case 17:
                        webClient.DownloadFile(
                            "http://www.dragonflybsd.org/download/",
                            @"c:\\ISO\\dragonflyBSD.iso");
                        IMGs.Items.Add("c:\\ISO\\dragonflyBSD.iso");
                        break;
                    case 18:
                        webClient.DownloadFile(
                            "https://sourceforge.net/projects/extix/files/latest/download",
                            @"c:\\ISO\\extix-22.12-64bit-deepin-20.8-refracta-2560mb-221218.iso");
                        IMGs.Items.Add("c:\\ISO\\extix-22.12-64bit-deepin-20.8-refracta-2560mb-221218.iso");
                        break;
                    case 19:
                        webClient.DownloadFile(
                            "https://sourceforge.net/projects/ferenoslinux/files/Feren-OS-standarddt.iso/download",
                            @"c:\\ISO\\FerenOS.iso");
                        IMGs.Items.Add("c:\\ISO\\FerenOS.iso");
                        break;
                    case 20:
                        webClient.DownloadFile(
                            "https://nobara.h3o66.de/iso/Nobara-37-Official-2023-02-05.iso",
                            @"c:\\ISO\\Nobara-37-Official-2023-02-05.iso");
                        IMGs.Items.Add("c:\\ISO\\Nobara-37-Official-2023-02-05.iso");
                        break;
                    case 21:
                        webClient.DownloadFile(
                            "https://ftp.qubes-os.org/iso/Qubes-R4.1.2-rc1-x86_64.iso",
                            @"c:\\ISO\\Qubes-R4.1.2-rc1-x86_64.iso");
                        IMGs.Items.Add("c:\\ISO\\Qubes-R4.1.2-rc1-x86_64.iso");
                        break;
                    case 22:
                        webClient.DownloadFile(
                            "https://download.neptuneos.com/download/Neptune75-20220814.iso",
                            @"c:\\ISO\\Neptune75-20220814.iso");
                        IMGs.Items.Add("c:\\ISO\\Neptune75-20220814.iso");
                        break;
                    case 23:
                        Uri URL = new Uri("https://sourceforge.net/projects/athena-iso/files/latest/download");
                        try
                        {

                            webClient.DownloadFileAsync(
                        URL,
                        @"c:\\iso\\athenaOS.iso");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        IMGs.Items.Add("c:\\ISO\\athenaOS.iso");
                        break;
                    case 24:

                        try
                        {
                            Stopwatch sw = new();
                            Global.stopwatch = sw;
                            sw.Start();

                            Uri URL2 = new Uri("https://ftp.linux.cz/pub/linux/slax/Slax-11.x/slax-64bit-11.6.0.iso");
                            webClient.DownloadFileAsync(URL2, @"c:\\ISO\\slax-64bit-11.6.0.iso");
                            IMGs.Items.Add("c:\\ISO\\slax-64bit-11.6.0.iso");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        break;


                    default:
                        break;
                }
            }
        }

        // The event that will fire whenever the progress of the WebClient is changed
        public void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Process ps = Global.ps;
            // Calculate download speed and output it to labelSpeed.
            Label Speed = new();
            // Time in hours, minutes, seconds
            //ps.StandardInput.WriteLine("echo off");

            //string x = string.Format("{0}    downloaded {1} of {2} bytes. {3} % complete...", (string)e.UserState, e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);

            //string x = (string.Format("echo downloadspeed: {0} kb/s..." + (e.BytesReceived / 1024d / Global.stopwatch.Elapsed.TotalSeconds).ToString("0.00")) + " downloaded: " + 
            //    string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"))).ToString();
            //MessageBox.Show(x, "Dimi was here");
            for (int i = 0; i < 100; i++)
            {
                ps.StandardInput.WriteLine(e.ProgressPercentage);
                if (i==e.ProgressPercentage) 
                {
                    //Global.ps.StandardInput.WriteLine("echo {0}    downloaded {1} of {2} bytes. {3} % complete...", (string)e.UserState, e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);
                }
            }
        }
        //}

        // The event that will trigger when the WebClient is completed
        public void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Process ps = Global.ps;
            // Reset the stopwatch.
            //sw.Reset();

            if (e.Cancelled == true)
            {
                ps.StandardInput.WriteLine("Download has been canceled.");
            }
            else
            {
                ps.StandardInput.WriteLine("Download completed!");
            }
        }

        private void copy2clip(object sender, MouseEventArgs e)
        {
            Clipboard.SetText(Command.Content.ToString());
            Process ps = Global.ps;
            ps.StandardInput.WriteLine("cls & echo " + Clipboard.GetText());
        }


        private void ShowTerminal(object sender, RoutedEventArgs e)
        {
            int hWnd = Global.Handle;
            ShowWindow(hWnd, SW_SHOW);
            ShowOutput.ToolTip = "Hide Terminal Output";
        }
        private void HideTerminal(object sender, RoutedEventArgs e)
        {
            int hWnd = Global.Handle;
            ShowWindow(hWnd, SW_HIDE);
            ShowOutput.ToolTip = "Show Terminal Output";
        }
    }
}
