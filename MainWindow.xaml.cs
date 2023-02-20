using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
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
        public bool HiddenTerminal { get; set; } = false;
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            Process ps = CreateProcess();
            Global.ps = ps;
            Global.id = ps.Id;
        }

        public Process CreateProcess()
        {
            Process ps = new();
            ps.StartInfo.RedirectStandardInput = true;
            ps.StartInfo.CreateNoWindow = HiddenTerminal;
            ps.StartInfo.FileName = "cmd.exe";
            ps.StartInfo.UseShellExecute = false;
            ps.StartInfo.WorkingDirectory = "c:\\pe__data";
            ps.Start();
            ps.StandardInput.WriteLine("echo off & cls");
            ps.StandardInput.WriteLine(":: cmd pid= " + ps.Id);
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
            if (!File.Exists("C:\\Program Files\\qemu\\qemu-system-x86_64.exe")) 
            { ps.StandardInput.WriteLine("powershell -command \"winget install qemu\""); }

            string fileName = IMGs.SelectedValue.ToString();
            if (fileName.Contains(".IMG") || fileName.Contains(".img"))
            { ps.StandardInput.WriteLine("\"C:\\Program Files\\qemu\\qemu-system-x86_64.exe\"  -m 10G -drive file=" + (char)34 + fileName + (char)34 + ",format=raw,index=0,media=disk -vga virtio -m 10G -no-reboot"); }
            if (fileName.Contains(".ISO") || fileName.Contains(".iso"))
            { ps.StandardInput.WriteLine("\"C:\\Program Files\\qemu\\qemu-system-x86_64.exe\" -cdrom " + (char)34 + fileName + (char)34 + " -m 10G"); }
            ShowWindow(hWnd, SW_HIDE);
            ShowOutput.IsChecked = false;
        }

        private void FindImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new();
            dlg.InitialDirectory = "c:\\pe__data\\";

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
            string removelistitem = IMGs.SelectedValue.ToString();
            for (int n = IMGs.Items.Count - 1; n >= 0; --n)
            {
                if (IMGs.Items[n].ToString().Contains(removelistitem))
                {
                    IMGs.Items.RemoveAt(n);

                    Process ps = Global.ps;
                    ps.StandardInput.WriteLine(":: Removing " + removelistitem);
                }
            }
        }

        private void ScanImages(object sender, RoutedEventArgs e)
        {
            IMGs.Items.Clear();
            foreach (var item in Directory.EnumerateFiles("c:\\PE__DATA", "*"))
            {
                if(item.EndsWith(".ISO") || 
                    item.EndsWith(".iso") || 
                    item.EndsWith(".IMG") || 
                    item.EndsWith(".img"))
                {
                    IMGs.Items.Add(item);
                    Process ps = Global.ps;
                    ps.StandardInput.WriteLine(":: Adding " + item);
                }
            }
            Scanner.IsEnabled = false;
            Scanner.Visibility = Visibility.Hidden;
            //ShowOutput.Visibility = Visibility.Visible;
        }

        private void ShowTerminal(object sender, RoutedEventArgs e)
        {
            ShowWindow(Global.Handle, SW_SHOW);
        }

        static string DownLoadFileInBackground2(System.Uri ImageFile)
        {
            // Create an HttpClientHandler object and set to use default credentials
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            // Create an HttpClient object
            HttpClient client = new HttpClient(handler);
            Task<HttpResponseMessage> response = client.GetAsync(ImageFile);
            try
            {
                if (response.IsCompletedSuccessfully)
                {
                    System.Runtime.CompilerServices.TaskAwaiter<HttpResponseMessage> responseBody = response.GetAwaiter();
                    //MessageBox.Show("response " + responseBody);
                    //return response.Result;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("\nException Caught!");
                MessageBox.Show("Message :{0} ", e.Message);
                //return e.Message;
            }
            Task<HttpResponseMessage> response2 = client.GetAsync(ImageFile);
            do
            {
                //nothing
            } while (response2.IsCompleted == false);
            if (response2.IsCompleted)
            {
                // Need to call dispose on the HttpClient and HttpClientHandler objects
                // when done using them, so the app doesn't leak resources
                //return response2.Result;
                //handler.Dispose();
                //client.Dispose();
            }
            return response.Result.ToString();

        }

        private static void DownloadFileCallback2(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Console.WriteLine("File download cancelled.");
            }

            if (e.Error != null)
            {
                Console.WriteLine(e.Error.ToString());
            }
        }

        //private static Task DownloadImage(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("First DownloadImgFunction");
        //    return DownloadImage(sender, e);
        //}

        private async void DownloadImage(object sender, RoutedEventArgs e)
        {
            if (ISOUrls.SelectedIndex.ToString()== "-1")
            {
                MessageBox.Show("please select an item first..");
            } 
            else 
            {
                int positionCombo = ISOUrls.SelectedIndex + 1;
                MessageBox.Show("position in combolist: " + (positionCombo).ToString());
                Process ps = Global.ps;
                string response = null;

                WebClient webClient = new();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCallback2);
                switch (positionCombo)
                {
                    case 1:
                        webClient.DownloadFile(
                            "https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_x64.iso", 
                            @"c:\\pe__data\\MX-21.3_x64.iso");
                        break;
                    case 2:
                        webClient.DownloadFile(
                            "https://mirror.alpix.eu/endeavouros/iso/EndeavourOS_Cassini_neo_22_12.iso", 
                            @"c:\\pe__data\\EndeavourOS_Cassini_neo_22_12.iso");
                        break;
                    case 3:
                        webClient.DownloadFile(
                            "https://mirror.crexio.com/linuxmint/isos/stable/21.1/linuxmint-21.1-cinnamon-64bit.iso", 
                            @"c:\\pe__data\\linuxmint-21.1-cinnamon-64bit.iso");
                        break;
                    case 4:
                        webClient.DownloadFile(
                            "https://download.manjaro.org/kde/22.0.3/manjaro-kde-22.0.3-230213-linux61.iso", 
                            @"c:\\pe__data\\manjaro-kde-22.0.3-230213-linux61.iso");
                        break;
                    case 5:
                        webClient.DownloadFile(
                            "https://download.manjaro.org/gnome/22.0.3/manjaro-gnome-22.0.3-230213-linux61.iso", 
                            @"c:\\pe__data\\manjaro-gnome-22.0.3-230213-linux61.iso");
                        break;
                    case 6:
                        webClient.DownloadFile(
                            "https://iso.pop-os.org/22.04/amd64/intel/22/pop-os_22.04_amd64_intel_22.iso", 
                            @"c:\\pe__data\\pop-os_22.04_amd64_intel_22.iso");
                        break;
                    case 7:
                        webClient.DownloadFile(
                            "https://iso.pop-os.org/22.04/amd64/nvidia/22/pop-os_22.04_amd64_nvidia_22.iso", 
                            @"c:\\pe__data\\pop-os_22.04_amd64_nvidia_22.iso");
                        break;
                    case 8:
                        webClient.DownloadFile(
                            "https://download.fedoraproject.org/pub/fedora/linux/releases/37/Workstation/x86_64/iso/Fedora-Workstation-Live-x86_64-37-1.7.iso", 
                            @"c:\\pe__data\\Fedora-Workstation-Live-x86_64-37-1.7.iso");
                        break;
                    case 9:
                        webClient.DownloadFile(
                            "https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-desktop-amd64.iso", 
                            @"c:\\pe__data\\ubuntu-22.04.1-desktop-amd64.iso");
                        break;
                    case 10:
                        webClient.DownloadFile(
                            "https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-live-server-amd64.iso", 
                            @"c:\\pe__data\\ubuntu-22.04.1-live-server-amd64.iso");
                        break;
                    case 11:
                        webClient.DownloadFile(
                            "https://cdimage.debian.org/debian-cd/current/amd64/iso-dvd/debian-11.6.0-amd64-DVD-1.iso", 
                            @"c:\\pe__data\\debian-11.6.0-amd64-DVD-1.iso");
                        break;
                    case 12:
                        webClient.DownloadFile(
                            "https://ftp.halifax.rwth-aachen.de/osdn/storage/g/l/li/linuxlite/6.2/linux-lite-6.2-64bit.iso",
                            @"c:\\pe__data\\linux-lite-6.2-64bit.iso");
                        break;
                    case 13:
                        webClient.DownloadFile(
                            "https://sourceforge.net/projects/garuda-linux/files/garuda/dr460nized/221019/garuda-dr460nized-linux-zen-221019.iso", 
                            @"c:\\pe__data\\garuda-dr460nized-linux-zen-221019.iso");
                        break;
                    case 14:
                        webClient.DownloadFile(
                            "https://ams3.dl.elementary.io/download/MTY3Njc4NTg3Nw==/elementaryos-7.0-stable.20230129rc.iso", 
                            @"c:\\pe__data\\elementaryos-7.0-stable.20230129rc.iso");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
