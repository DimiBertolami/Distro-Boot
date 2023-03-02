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
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QemuUtil.Entities;

namespace QemuUtil
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
            InitializeComponent();
            Process ps = CreateProcess();
            int id = 0;
            Global.ps = ps;
            Global.id = ps.Id;
            int hWnd = ps.MainWindowHandle.ToInt32();
            Global.Handle = hWnd;
            //getInfo();
            new Image(0, "c:\\ISO\\MX-21.3_ahs_x64.iso", "https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_ahs_x64.iso/download");
            new Image(1, "c:\\ISO\\EndeavourOS_Cassini_neo_22_12.iso", "https://mirror.alpix.eu/endeavouros/iso/EndeavourOS_Cassini_neo_22_12.iso");
            new Image(2, "c:\\ISO\\linuxmint-21.1-cinnamon-64bit.iso", "https://mirror.crexio.com/linuxmint/isos/stable/21.1/linuxmint-21.1-cinnamon-64bit.iso");
            new Image(3, "c:\\ISO\\manjaro-kde-22.0.3-230213-linux61.iso", "https://download.manjaro.org/kde/22.0.3/manjaro-kde-22.0.3-230213-linux61.iso");
            new Image(4, "c:\\ISO\\manjaro-gnome-22.0.3-230213-linux61.iso", "https://download.manjaro.org/gnome/22.0.3/manjaro-gnome-22.0.3-230213-linux61.iso");
            new Image(5, "c:\\ISO\\pop-os_22.04_amd64_intel_22.iso", "https://iso.pop-os.org/22.04/amd64/intel/22/pop-os_22.04_amd64_intel_22.iso");
            new Image(6, "c:\\ISO\\pop-os_22.04_amd64_nvidia_22.iso", "https://iso.pop-os.org/22.04/amd64/nvidia/22/pop-os_22.04_amd64_nvidia_22.iso");
            new Image(7, "c:\\ISO\\Fedora-Workstation-Live-x86_64-37-1.7.iso", "https://download.fedoraproject.org/pub/fedora/linux/releases/37/Workstation/x86_64/iso/Fedora-Workstation-Live-x86_64-37-1.7.iso");
            new Image(8, "c:\\ISO\\ubuntu-22.04.1-desktop-amd64.iso", "https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-desktop-amd64.iso");
            new Image(9, "c:\\ISO\\ubuntu-22.04.1-live-server-amd64.iso", "https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-live-server-amd64.iso");
            new Image(10, "c:\\ISO\\debian-11.6.0-amd64-DVD-1.iso", "https://cdimage.debian.org/debian-cd/current/amd64/iso-dvd/debian-11.6.0-amd64-DVD-1.iso");
            new Image(11, "c:\\ISO\\Linux-lite-6.2-64bit.iso", "https://ftp.halifax.rwth-aachen.de/osdn/storage/g/l/li/linuxlite/6.2/linux-lite-6.2-64bit.iso");
            new Image(12, "c:\\ISO\\garuda-dr460nized-linux-zen-221019.iso", "https://sourceforge.net/projects/garuda-linux/files/garuda/dr460nized/221019/garuda-dr460nized-linux-zen-221019.iso");
            new Image(13, "c:\\ISO\\elementaryos-7.0-stable.20230129rc.iso", "https://ams3.dl.elementary.io/download/MTY3Njc4NTg3Nw==/elementaryos-7.0-stable.20230129rc.iso");
            new Image(14, "c:\\ISO\\voyagerlive.iso", "https://sourceforge.net/projects/voyagerlive/files/latest/download");
            new Image(15, "c:\\ISO\\AV_Linux_MX_Edition-21.3_ahs_x64.iso", "https://downloads.bandshed.net/AVL-MXE_21.3/AV_Linux_MX_Edition-21.3_ahs_x64.iso");
            new Image(16, "c:\\ISO\\haiku-r1beta4-x86_64-anyboot.iso", "http://mirror.rit.edu/haiku/r1beta4/haiku-r1beta4-x86_64-anyboot.iso");
            new Image(17, "c:\\ISO\\dfly-x86_64-6.4.0_REL.iso", "https://mirror-master.dragonflybsd.org/iso-images/dfly-x86_64-6.4.0_REL.iso");
            new Image(18, "c:\\ISO\\extix-22.12-64bit-deepin-20.8-refracta-2560mb-221218.iso", "https://sourceforge.net/projects/extix/files/latest/download");
            new Image(19, "c:\\ISO\\FerenOS.iso", "https://sourceforge.net/projects/ferenoslinux/files/Feren-OS-standarddt.iso/download");
            new Image(20, "c:\\ISO\\Nobara-37-Official-2023-02-05.iso", "https://nobara.h3o66.de/iso/Nobara-37-Official-2023-02-05.iso");
            new Image(21, "c:\\ISO\\Qubes-R4.1.2-rc1-x86_64.iso", "https://ftp.qubes-os.org/iso/Qubes-R4.1.2-rc1-x86_64.iso");
            new Image(22, "c:\\ISO\\Neptune75-20220814.iso", "https://download.neptuneos.com/download/Neptune75-20220814.iso");
            new Image(23, "c:\\ISO\\athenaOS.iso", "https://sourceforge.net/projects/athena-iso/files/latest/download");
            new Image(24, "c:\\ISO\\slax-64bit-11.6.0.iso", "https://ftp.linux.cz/pub/linux/slax/Slax-11.x/slax-64bit-11.6.0.iso");
            new Image(25, "c:\\ISO\\Zorin-OS-16.2-Core-64-bit-r1.iso", "https://ftp.halifax.rwth-aachen.de/zorinos/16/Zorin-OS-16.2-Core-64-bit-r1.iso");
            new Image(26, "c:\\ISO\\kali-linux-2022.4-installer-amd64.iso", "https://cdimage.kali.org/kali-2022.4/kali-linux-2022.4-installer-amd64.iso");
            new Image(27, "c:\\ISO\\cld-23-x86_64.iso", "https://mirror.koddos.net/calculate-linux/release/23/cld-23-x86_64.iso");
            new Image(28, "c:\\ISO\\CorePlus-current.iso", "http://www.tinycorelinux.net/13.x/x86/release/CorePlus-current.iso");
            new Image(29, "c:\\ISO\\OracleLinux-R9-U1-x86_64-dvd.iso", "https://yum.oracle.com/ISOS/OracleLinux/OL9/u1/x86_64/OracleLinux-R9-U1-x86_64-dvd.iso");
            new Image(30, "c:\\ISO\\installOpenBSD72.iso", "https://ftp.fr.openbsd.org/pub/OpenBSD/7.2/amd64/install72.iso");
            new Image(31, "c:\\ISO\\ubuntustudio-22.04.2-dvd-amd64.iso", "https://cdimage.ubuntu.com/ubuntustudio/releases/22.04.2/release/ubuntustudio-22.04.2-dvd-amd64.iso");
            new Image(32, "c:\\ISO\\LXLE-Focal-Release.iso", "https://sourceforge.net/projects/lxle/files/Final/OS/Focal-64/LXLE-Focal-Release.iso/download");
            new Image(33, "c:\\ISO\\GeckoLinux_STATIC_Cinnamon.x86_64-154.220822.0.iso", "https://sourceforge.net/projects/geckolinux/files/Static/154.220822/GeckoLinux_STATIC_Cinnamon.x86_64-154.220822.0.iso/download");
            new Image(34, "c:\\ISO\\Peropesis-2.0-live.iso", "https://peropesis.org/peropesis/Peropesis-2.0-live.iso");
            new Image(35, "c:\\ISO\\rebornos_iso-2023.01.20-x86_64.iso", "https://pub-236acba899534258ba381237ad7d8714.r2.dev/RebornOS-ISO/rebornos_iso-2023.01.20-x86_64.iso");
            new Image(36, "c:\\ISO\\MakuluLinux-ShiFt-U-2022-12-29.iso", "https://sourceforge.net/projects/makulu/files/downloads/Shift/MakuluLinux-ShiFt-U-2022-12-29.iso/download");
            new Image(37, "c:\\ISO\\kodachi-8.27-64-kernel-6.2.iso", "https://sourceforge.net/projects/linuxkodachi/files/kodachi-8.27-64-kernel-6.2.iso/download");
            new Image(38, "c:\\ISO\\bodhi-6.0.0-64-apppack.iso", "https://sourceforge.net/projects/bodhilinux/files/6.0.0/bodhi-6.0.0-64-apppack.iso/download");
            new Image(39, "c:\\ISO\\TUXEDO-OS-2-202302271716.iso", "https://os.tuxedocomputers.com/TUXEDO-OS-2-202302271716.iso");
            new Image(40, "c:\\ISO\\tails-amd64-5.10.iso", "https://download.tails.net/tails/stable/tails-amd64-5.10/tails-amd64-5.10.iso");
            new Image(41, "c:\\ISO\\slackware64-15.0-install-dvd.iso", "https://mirrors.slackware.com/slackware/slackware-iso/slackware64-15.0-iso/slackware64-15.0-install-dvd.iso");
            new Image(42, "c:\\ISO\\linuxfx-11.2.22.04.7-win10-theme-cinnamon-wxd-13.0.iso", "https://sourceforge.net/projects/linuxfxdevil/files/linuxfx-11.2.22.04.7-win10-theme-cinnamon-wxd-13.0.iso/download");
            new Image(43, "c:\\ISO\\linuxfx-11.2.22.04.7-win11-theme-plasma-wxd-13.0.iso", "https://sourceforge.net/projects/linuxfxdevil/files/linuxfx-11.2.22.04.7-win11-theme-plasma-wxd-13.0.iso/download");
            new Image(44, "c:\\ISO\\fossapup64-9.5.iso", "https://distro.ibiblio.org/puppylinux/puppy-fossa/fossapup64-9.5.iso");
            new Image(45, "c:\\ISO\\arcolinuxb-awesome-v23.03.01-x86_64.iso", "https://ftp.belnet.be/arcolinux/iso/v23.03.01/arcolinuxb-awesome-v23.03.01-x86_64.iso");
            new Image(46, "c:\\ISO\\easy-5.0-amd64.iso", "https://distro.ibiblio.org/easyos/amd64/releases/kirkstone/2023/5.0/easy-5.0-amd64.img");
            new Image(47, "c:\\ISO\\nitrux-nx-desktop-d5c7cdff-amd64.iso", "https://sourceforge.net/projects/nitruxos/files/Release/ISO/nitrux-nx-desktop-d5c7cdff-amd64.iso/download");
            new Image(48, "c:\\ISO\\Rocky-9.1-x86_64-dvd.iso", "https://download.rockylinux.org/pub/rocky/9/isos/x86_64/Rocky-9.1-x86_64-dvd.iso");
            new Image(49, "c:\\ISO\\salix64-xfce-15.0.iso", "https://sourceforge.net/projects/salix/files/15.0/salix64-xfce-15.0.iso/download");
            new Image(50, "c:\\ISO\\devuan_beowulf_3.1.1_amd64_desktop.iso", "https://files.devuan.org/devuan_beowulf/installer-iso/devuan_beowulf_3.1.1_amd64_desktop.iso");
            new Image(51, "c:\\ISO\\livegui-amd64-20230226T163212Z.iso", "https://ftp.belnet.be/pub/rsync.gentoo.org/gentoo/releases/amd64/autobuilds/current-livegui-amd64/livegui-amd64-20230226T163212Z.iso");
            new Image(52, "c:\\ISO\\pureos-10~devel-gnome-live-20220602_amd64.iso", "https://downloads.puri.sm/byzantium/gnome/2022-06-02/pureos-10~devel-gnome-live-20220602_amd64.iso");
            new Image(53, "c:\\ISO\\ELD-9-x86_64-latest.iso", "https://fbi.cdn.euro-linux.com/isos/ELD-9-x86_64-latest.iso");
            new Image(54, "c:\\ISO\\MX-21.3_x64.iso", "https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_x64.iso/download");
            new Image(55, "c:\\ISO\\primeos.iso", "https://sourceforge.net/projects/primeos/files/latest/download");
            new Image(56, "c:\\ISO\\blissos.iso", "https://sourceforge.net/projects/blissos-dev/files/latest/download");
            new Image(57, "c:\\ISO\\ReactOS-0.4.14-release-57-g1e953d8-iso.zip", "https://sourceforge.net/projects/reactos/files/latest/download");
                //DlDistro("https://sourceforge.net/projects/reactos/files/latest/download", "c:\\ISO\\ReactOS-0.4.14-release-57-g1e953d8-iso.zip");
                //Global.ps.StandardInput.WriteLine("PowerShell -Command \"Expand-Archive -Path c:\\ISO\\ReactOS-0.4.14-release-57-g1e953d8-iso.zip -DestinationPath C:\\ISO -Force\"");
                //File.Delete(@"c:\\ISO\\ReactOS-0.4.14-release-57-g1e953d8-iso.zip");
                //IMGs.Items.Add("c:\\ISO\\ReactOS-0.4.14-release-57-g1e953d8.iso");

            new Image(58, "c:\\ISO\\peppermintos.iso", "https://sourceforge.net/projects/peppermintos/files/latest/download");
            new Image(59, "c:\\ISO\\clear-37980-live-desktop.iso", "https://cdn.download.clearlinux.org/releases/37980/clear/clear-37980-live-desktop.iso");
            new Image(60, "c:\\ISO\\Redcore.Linux.Hardened.2301.Sirius.KDE.amd64.iso", "http://ro.mirror.redcorelinux.org/amd64/iso/Redcore.Linux.Hardened.2301.Sirius.KDE.amd64.iso");
            new Image(61, "c:\\ISO\\locos.iso", "https://sourceforge.net/projects/loc-os/files/Loc-OS%2022/LXDE/Loc-OS-22-LXDE-x86_64.iso/download");
            new Image(62, "c:\\ISO\\mabox.iso", "https://sourceforge.net/projects/mabox-linux/files/latest/download");
            new Image(63, "c:\\ISO\\reloadedos.iso", "https://netcologne.dl.sourceforge.net/project/reloaded-caf/ginkgo/Reloaded-13.0-ginkgo-OFFICIAL-20221113-GAPPS.zip");
            new Image(64, "c:\\ISO\\emmabuntus.iso", "https://sourceforge.net/projects/emmabuntus/files/latest/download");
            new Image(65, "c:\\ISO\\arcolinuxb-xtended-v23.03.01-x86_64.iso", "https://sourceforge.net/projects/arcolinux-community-editions/files/xtended/arcolinuxb-xtended-v23.03.01-x86_64.iso/download");
            new Image(66, "c:\\ISO\\openmandriva.iso", "https://sourceforge.net/projects/openmandriva/files/latest/download");
            new Image(67, "c:\\ISO\\legacyoslinux.iso", "https://sourceforge.net/projects/legacyoslinux/files/latest/download");
            new Image(68, "c:\\ISO\\Xiaopan%206.4.1.zip", "https://netcologne.dl.sourceforge.net/project/xiaopanos/Xiaopan%206.4.1.zip");
            new Image(69, "c:\\ISO\\cachyos.iso", "https://sourceforge.net/projects/cachyos-arch/files/latest/download");

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
            Global.ps = ps;
            Global.id = ps.Id;
            Global.Handle = ps.MainWindowHandle.ToInt32();
            int hWnd = ps.MainWindowHandle.ToInt32();
            ShowWindow(hWnd, SW_HIDE);
            return ps;
        }

        //private void copy2clip(object sender, MouseEventArgs e)
        //{
        //    Clipboard.SetText(Command.Content.ToString());
        //    Process ps = Global.ps;
        //    ps.StandardInput.WriteLine("cls & echo " + Clipboard.GetText());
        //}
        public void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Process ps = Global.ps;
            string Downloaded = string.Format("{0}    downloaded {1} of {2} bytes. {3} % complete...", (string)e.UserState, e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);
            MessageBox.Show(Downloaded);
            string DlSpeed = string.Format("echo downloadspeed: {0} kb/s..." + (e.BytesReceived / 1024d / Global.stopwatch.Elapsed.TotalSeconds).ToString("0.00"));
            Global.ps.StandardInput.WriteLine(DlSpeed.ToString() + " - " + Downloaded.ToString());
            Command.Content = DlSpeed + " " + Downloaded;
            MessageBox.Show(DlSpeed + " " + Downloaded);
        }


        public void Completed(object sender, AsyncCompletedEventArgs e)
        {
            Process ps = Global.ps;
            if (e.Cancelled == true)
            {
                ps.StandardInput.WriteLine("Download has been canceled.");
            }
            else
            {
                ps.StandardInput.WriteLine("Download complete!");
            }
        }

        private void BootHidden(object sender, MouseButtonEventArgs e)
        {
            Process ps = Global.ps;
            int hWnd = ps.MainWindowHandle.ToInt32();
            Global.Handle = hWnd;
            ShowWindow(hWnd, SW_HIDE);
            if (!File.Exists("C:\\Program Files\\qemu\\qemu-system-x86_64.exe"))
            { ps.StandardInput.WriteLine("powershell -command \"winget install qemu --force\""); }
            DistroBoot.WindowState = WindowState.Minimized;
            string ?fileName = IMGs.SelectedValue.ToString();
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
                DistroBoot.WindowState = WindowState.Normal;
                DistroBoot.UpdateLayout();
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
                using (FileStream fs = File.Create("list.txt"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(fileName);
                    fs.Write(info, 0, info.Length);
                }
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
            hWnd = Global.ps.MainWindowHandle.ToInt32();
            ShowWindow(hWnd, SW_HIDE);

            int Counter = 0;
            //if (File.Exists("list.txt"))
            //{
            //    // Open the stream and read it back.
            //    using (StreamReader sr = File.OpenText("list.txt"))
            //    {
            //        string s = "";
            //        while (sr.ReadLine() != null)
            //        {
            //            IMGs.Items.Add(s);
            //        }

            //    }
            IMGs.Items.Clear();
            foreach (var item in Directory.EnumerateFiles("c:\\ISO", "*"))
            {
                if (item.ToUpper().EndsWith(".ISO") || item.ToUpper().EndsWith(".IMG"))
                {
                    IMGs.Items.Add(item);
                    Process ps = Global.ps;
                }
            }
                //Scanner.IsEnabled = false;
                //Scanner.Visibility = Visibility.Hidden;
            //}
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

        public void DlDistro(string url, string fileName)
        {
            WebClient webClient = new();
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            webClient.DownloadFile(url, @fileName);
            IMGs.Items.Add(fileName);
        }

        private void DownloadImage(object sender, RoutedEventArgs e)
        {
            Process ps = Global.ps;

            ps.StandardInput.WriteLine("echo " + ISOUrls.SelectedIndex.ToString());
            if (ISOUrls.SelectedIndex.ToString() == "-1")
            {
                MessageBox.Show("please select an item first..");
            }
            else
            {
                int positionCombo = ISOUrls.SelectedIndex;
                Stopwatch sw = new();
                sw.Start();
                ps.StandardInput.WriteLine("echo " + (positionCombo).ToString() + " - - " + ISOUrls.Items.GetItemAt(positionCombo).ToString());
                WebClient webClient = new();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);

                switch (positionCombo)
                {
                    case 0:
                        DlDistro("https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_ahs_x64.iso/download", "c:\\ISO\\MX-21.3_ahs_x64.iso");
                        break;
                    case 1:
                        DlDistro("https://mirror.alpix.eu/endeavouros/iso/EndeavourOS_Cassini_neo_22_12.iso", "c:\\ISO\\EndeavourOS_Cassini_neo_22_12.iso");
                        break;
                    case 2:
                        DlDistro("https://mirror.crexio.com/linuxmint/isos/stable/21.1/linuxmint-21.1-cinnamon-64bit.iso", "c:\\ISO\\linuxmint-21.1-cinnamon-64bit.iso");
                        break;
                    case 3:
                        DlDistro("https://download.manjaro.org/kde/22.0.3/manjaro-kde-22.0.3-230213-linux61.iso", "c:\\ISO\\manjaro-kde-22.0.3-230213-linux61.iso");
                        break;
                    case 4:
                        DlDistro("https://download.manjaro.org/gnome/22.0.3/manjaro-gnome-22.0.3-230213-linux61.iso", "c:\\ISO\\manjaro-gnome-22.0.3-230213-linux61.iso");
                        break;
                    case 5:
                        DlDistro("https://iso.pop-os.org/22.04/amd64/intel/22/pop-os_22.04_amd64_intel_22.iso", "c:\\ISO\\pop-os_22.04_amd64_intel_22.iso");
                        break;
                    case 6:
                        DlDistro("https://iso.pop-os.org/22.04/amd64/nvidia/22/pop-os_22.04_amd64_nvidia_22.iso", "c:\\ISO\\pop-os_22.04_amd64_nvidia_22.iso");
                        break;
                    case 7:
                        DlDistro("https://download.fedoraproject.org/pub/fedora/linux/releases/37/Workstation/x86_64/iso/Fedora-Workstation-Live-x86_64-37-1.7.iso", "c:\\ISO\\Fedora-Workstation-Live-x86_64-37-1.7.iso");
                        break;
                    case 8:
                        DlDistro("https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-desktop-amd64.iso", "c:\\ISO\\ubuntu-22.04.1-desktop-amd64.iso");
                        break;
                    case 9:
                        DlDistro("https://releases.ubuntu.com/22.04.1/ubuntu-22.04.1-live-server-amd64.iso", "c:\\ISO\\ubuntu-22.04.1-live-server-amd64.iso");
                        break;
                    case 10:
                        DlDistro("https://cdimage.debian.org/debian-cd/current/amd64/iso-dvd/debian-11.6.0-amd64-DVD-1.iso", "c:\\ISO\\debian-11.6.0-amd64-DVD-1.iso");
                        break;
                    case 11:
                        DlDistro("https://ftp.halifax.rwth-aachen.de/osdn/storage/g/l/li/linuxlite/6.2/linux-lite-6.2-64bit.iso", "c:\\ISO\\Linux-lite-6.2-64bit.iso");
                        break;
                    case 12:
                        DlDistro("https://sourceforge.net/projects/garuda-linux/files/garuda/dr460nized/221019/garuda-dr460nized-linux-zen-221019.iso", "c:\\ISO\\garuda-dr460nized-linux-zen-221019.iso");
                        break;
                    case 13:
                        DlDistro("https://ams3.dl.elementary.io/download/MTY3Njc4NTg3Nw==/elementaryos-7.0-stable.20230129rc.iso", "c:\\ISO\\elementaryos-7.0-stable.20230129rc.iso");
                        break;
                    case 14:
                        DlDistro("https://sourceforge.net/projects/voyagerlive/files/latest/download", "c:\\ISO\\voyagerlive.iso");
                        break;
                    case 15:
                        DlDistro("https://downloads.bandshed.net/AVL-MXE_21.3/AV_Linux_MX_Edition-21.3_ahs_x64.iso", "c:\\ISO\\AV_Linux_MX_Edition-21.3_ahs_x64.iso");
                        break;
                    case 16:
                        DlDistro("http://mirror.rit.edu/haiku/r1beta4/haiku-r1beta4-x86_64-anyboot.iso", "c:\\ISO\\haiku-r1beta4-x86_64-anyboot.iso");
                        break;
                    case 17:
                        DlDistro("https://mirror-master.dragonflybsd.org/iso-images/dfly-x86_64-6.4.0_REL.iso", "c:\\ISO\\dfly-x86_64-6.4.0_REL.iso");
                        break;
                    case 18:
                        DlDistro("https://sourceforge.net/projects/extix/files/latest/download", "c:\\ISO\\extix-22.12-64bit-deepin-20.8-refracta-2560mb-221218.iso");
                        break;
                    case 19:
                        DlDistro("https://sourceforge.net/projects/ferenoslinux/files/Feren-OS-standarddt.iso/download", "c:\\ISO\\FerenOS.iso");
                        break;
                    case 20:
                        DlDistro("https://nobara.h3o66.de/iso/Nobara-37-Official-2023-02-05.iso", "c:\\ISO\\Nobara-37-Official-2023-02-05.iso");
                        break;
                    case 21:
                        DlDistro("https://ftp.qubes-os.org/iso/Qubes-R4.1.2-rc1-x86_64.iso", "c:\\ISO\\Qubes-R4.1.2-rc1-x86_64.iso");
                        break;
                    case 22:
                        DlDistro("https://download.neptuneos.com/download/Neptune75-20220814.iso", "c:\\ISO\\Neptune75-20220814.iso");
                        break;
                    case 23:
                        DlDistro("https://sourceforge.net/projects/athena-iso/files/latest/download", "c:\\ISO\\athenaOS.iso");
                        break;
                    case 24:
                        DlDistro("https://ftp.linux.cz/pub/linux/slax/Slax-11.x/slax-64bit-11.6.0.iso", "c:\\ISO\\slax-64bit-11.6.0.iso");
                        break;
                    case 25:
                        DlDistro("https://ftp.halifax.rwth-aachen.de/zorinos/16/Zorin-OS-16.2-Core-64-bit-r1.iso", "c:\\ISO\\Zorin-OS-16.2-Core-64-bit-r1.iso");
                        break;
                    case 26:
                        DlDistro("https://cdimage.kali.org/kali-2022.4/kali-linux-2022.4-installer-amd64.iso", "c:\\ISO\\kali-linux-2022.4-installer-amd64.iso");
                        break;
                    case 27:
                        DlDistro("https://mirror.koddos.net/calculate-linux/release/23/cld-23-x86_64.iso", "c:\\ISO\\cld-23-x86_64.iso");
                        break;
                    case 28:
                        DlDistro("http://www.tinycorelinux.net/13.x/x86/release/CorePlus-current.iso", "c:\\ISO\\CorePlus-current.iso");
                        break;
                    case 29:
                        DlDistro("https://yum.oracle.com/ISOS/OracleLinux/OL9/u1/x86_64/OracleLinux-R9-U1-x86_64-dvd.iso", "c:\\ISO\\OracleLinux-R9-U1-x86_64-dvd.iso");
                        break;
                    case 30:
                        DlDistro("https://ftp.fr.openbsd.org/pub/OpenBSD/7.2/amd64/install72.iso", "c:\\ISO\\installOpenBSD72.iso");
                        break;
                    case 31:
                        DlDistro("https://cdimage.ubuntu.com/ubuntustudio/releases/22.04.2/release/ubuntustudio-22.04.2-dvd-amd64.iso", "c:\\ISO\\ubuntustudio-22.04.2-dvd-amd64.iso");
                        break;
                    case 32:
                        DlDistro("https://sourceforge.net/projects/lxle/files/Final/OS/Focal-64/LXLE-Focal-Release.iso/download", "c:\\ISO\\LXLE-Focal-Release.iso");
                        break;
                    case 33:
                        DlDistro("https://sourceforge.net/projects/geckolinux/files/Static/154.220822/GeckoLinux_STATIC_Cinnamon.x86_64-154.220822.0.iso/download", "c:\\ISO\\GeckoLinux_STATIC_Cinnamon.x86_64-154.220822.0.iso");
                        break;
                    case 34:
                        DlDistro("https://peropesis.org/peropesis/Peropesis-2.0-live.iso", "c:\\ISO\\Peropesis-2.0-live.iso");
                        break;
                    case 35:
                        DlDistro("https://pub-236acba899534258ba381237ad7d8714.r2.dev/RebornOS-ISO/rebornos_iso-2023.01.20-x86_64.iso", "c:\\ISO\\rebornos_iso-2023.01.20-x86_64.iso");
                        break;
                    case 36:
                        DlDistro("https://sourceforge.net/projects/makulu/files/downloads/Shift/MakuluLinux-ShiFt-U-2022-12-29.iso/download", "c:\\ISO\\MakuluLinux-ShiFt-U-2022-12-29.iso");
                        break;
                    case 37:
                        DlDistro("https://sourceforge.net/projects/linuxkodachi/files/kodachi-8.27-64-kernel-6.2.iso/download", "c:\\ISO\\kodachi-8.27-64-kernel-6.2.iso");
                        break;
                    case 38:
                        DlDistro("https://sourceforge.net/projects/bodhilinux/files/6.0.0/bodhi-6.0.0-64-apppack.iso/download", "c:\\ISO\\bodhi-6.0.0-64-apppack.iso");
                        break;
                    case 39:
                        DlDistro("https://os.tuxedocomputers.com/TUXEDO-OS-2-202302271716.iso", "c:\\ISO\\TUXEDO-OS-2-202302271716.iso");
                        break;
                    case 40:
                        DlDistro("https://download.tails.net/tails/stable/tails-amd64-5.10/tails-amd64-5.10.iso", "c:\\ISO\\tails-amd64-5.10.iso");
                        break;
                    case 41:
                        DlDistro("https://mirrors.slackware.com/slackware/slackware-iso/slackware64-15.0-iso/slackware64-15.0-install-dvd.iso", "c:\\ISO\\slackware64-15.0-install-dvd.iso");
                        break;
                    case 42:
                        DlDistro("https://sourceforge.net/projects/linuxfxdevil/files/linuxfx-11.2.22.04.7-win10-theme-cinnamon-wxd-13.0.iso/download", "c:\\ISO\\linuxfx-11.2.22.04.7-win10-theme-cinnamon-wxd-13.0.iso");
                        break;
                    case 43:
                        DlDistro("https://sourceforge.net/projects/linuxfxdevil/files/linuxfx-11.2.22.04.7-win11-theme-plasma-wxd-13.0.iso/download", "c:\\ISO\\linuxfx-11.2.22.04.7-win11-theme-plasma-wxd-13.0.iso");
                        break;
                    case 44:
                        DlDistro("https://distro.ibiblio.org/puppylinux/puppy-fossa/fossapup64-9.5.iso", "c:\\ISO\\fossapup64-9.5.iso");
                        break;
                    case 45:
                        DlDistro("https://ftp.belnet.be/arcolinux/iso/v23.03.01/arcolinuxb-awesome-v23.03.01-x86_64.iso", "c:\\ISO\\arcolinuxb-awesome-v23.03.01-x86_64.iso");
                        break;
                    case 46:
                        DlDistro("https://distro.ibiblio.org/easyos/amd64/releases/kirkstone/2023/5.0/easy-5.0-amd64.img", "c:\\ISO\\easy-5.0-amd64.img");
                        break;
                    case 47:
                        DlDistro("https://sourceforge.net/projects/nitruxos/files/Release/ISO/nitrux-nx-desktop-d5c7cdff-amd64.iso/download", "c:\\ISO\\nitrux-nx-desktop-d5c7cdff-amd64.iso");
                        break;
                    case 48:
                        DlDistro("https://download.rockylinux.org/pub/rocky/9/isos/x86_64/Rocky-9.1-x86_64-dvd.iso", "c:\\ISO\\Rocky-9.1-x86_64-dvd.iso");
                        break;
                    case 49:
                        DlDistro("https://sourceforge.net/projects/salix/files/15.0/salix64-xfce-15.0.iso/download", "c:\\ISO\\salix64-xfce-15.0.iso");
                        break;
                    case 50:
                        DlDistro("https://files.devuan.org/devuan_beowulf/installer-iso/devuan_beowulf_3.1.1_amd64_desktop.iso", "c:\\ISO\\devuan_beowulf_3.1.1_amd64_desktop.iso");
                        break;
                    case 51:
                        DlDistro("https://ftp.belnet.be/pub/rsync.gentoo.org/gentoo/releases/amd64/autobuilds/current-livegui-amd64/livegui-amd64-20230226T163212Z.iso", "c:\\ISO\\livegui-amd64-20230226T163212Z.iso");
                        break;
                    case 52:
                        DlDistro("https://downloads.puri.sm/byzantium/gnome/2022-06-02/pureos-10~devel-gnome-live-20220602_amd64.iso", "c:\\ISO\\pureos-10~devel-gnome-live-20220602_amd64.iso");
                        break;
                    case 53:
                        DlDistro("https://fbi.cdn.euro-linux.com/isos/ELD-9-x86_64-latest.iso", "c:\\ISO\\ELD-9-x86_64-latest.iso");
                        break;
                    case 54:
                        DlDistro("https://sourceforge.net/projects/mx-linux/files/Final/Xfce/MX-21.3_x64.iso/download", "c:\\ISO\\MX-21.3_x64.iso");
                        break;
                    case 55:
                        DlDistro("https://sourceforge.net/projects/primeos/files/latest/download", "c:\\ISO\\primeos.iso");
                        break;
                    case 56:
                        DlDistro("https://sourceforge.net/projects/blissos-dev/files/latest/download", "c:\\ISO\\blissos.iso");
                        break;
                    case 57:
                        DlDistro("https://sourceforge.net/projects/reactos/files/latest/download", "c:\\ISO\\reactos.iso");
                        break;
                    case 58:
                        DlDistro("https://sourceforge.net/projects/peppermintos/files/latest/download", "c:\\ISO\\peppermintos.iso");
                        break;
                    case 59:
                        DlDistro("https://cdn.download.clearlinux.org/releases/37980/clear/clear-37980-live-desktop.iso", "c:\\ISO\\clear-37980-live-desktop.iso");
                        break;
                    case 60:
                        DlDistro("http://ro.mirror.redcorelinux.org/amd64/iso/Redcore.Linux.Hardened.2301.Sirius.KDE.amd64.iso", "c:\\ISO\\Redcore.Linux.Hardened.2301.Sirius.KDE.amd64.iso");
                        break;
                    case 61:
                        DlDistro("https://sourceforge.net/projects/loc-os/files/Loc-OS%2022/LXDE/Loc-OS-22-LXDE-x86_64.iso/download", "c:\\ISO\\locos.iso");
                        break;
                    case 62:
                        DlDistro("https://sourceforge.net/projects/mabox-linux/files/latest/download", "c:\\ISO\\mabox.iso");
                        break;
                    case 63:
                        DlDistro("https://netcologne.dl.sourceforge.net/project/reloaded-caf/ginkgo/Reloaded-13.0-ginkgo-OFFICIAL-20221113-GAPPS.zip", "c:\\ISO\\reloadedos.iso");
                        break;
                    case 64:
                        DlDistro("https://sourceforge.net/projects/emmabuntus/files/latest/download", "c:\\ISO\\emmabuntus.iso");
                        break;
                    case 65:
                        DlDistro("https://sourceforge.net/projects/arcolinux-community-editions/files/xtended/arcolinuxb-xtended-v23.03.01-x86_64.iso/download", "c:\\ISO\\arcolinuxb-xtended-v23.03.01-x86_64.iso");
                        break;
                    case 66:
                        DlDistro("https://sourceforge.net/projects/openmandriva/files/latest/download", "c:\\ISO\\openmandriva.iso");
                        break;
                    case 67:
                        DlDistro("https://sourceforge.net/projects/legacyoslinux/files/latest/download", "c:\\ISO\\legacyoslinux.iso");
                        break;
                    case 68:
                        webClient.DownloadFile("https://netcologne.dl.sourceforge.net/project/xiaopanos/Xiaopan%206.4.1.zip",
                                @"c:\\ISO\\Xiaopan%206.4.1.zip");
                        Global.ps.StandardInput.WriteLine("PowerShell -Command \"Expand-Archive -Path Xiaopan%206.4.1.zip -DestinationPath C:\\ISO -Force\"");
                        File.Delete(@"c:\\ISO\\Xiaopan%206.4.1.zip");
                        IMGs.Items.Add("c:\\ISO\\Xiaopan6.4.1.iso");
                        break;
                    case 69:
                        DlDistro("https://sourceforge.net/projects/cachyos-arch/files/latest/download", "c:\\ISO\\cachyos.iso");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

//https://mirrors.fossho.st/draugeros/download/ISOs/Drauger_OS-7.6-AMD64.iso

//Crypto - OS
//https://cryptocurrencyos.org/downloads/Techrich-Crypto-Disc-Bullrun-21777.iso

//AllegianceOS
//https://sourceforge.net/projects/allegianceos/files/ISOs/aos-beta-3.1-x86_64.iso/download

//https://sourceforge.net/projects/hackman-linux/files/2022/kde/StormOS_KDE-2023.02.24-x86_64.iso/download

//https://netix.dl.sourceforge.net/project/quemos/latest/QuemOS1.4x86_64.iso

//https://sourceforge.net/projects/lionlinux/files/Leyla%202.0/lionlxde_x64_v2.iso/download

//https://sourceforge.net/projects/shinyos/files/ShinyOS-V1-desktop-amd64.iso/download

//https://sourceforge.net/projects/titan-linux/files/latest/download

//https://sourceforge.net/projects/crowz/files/crowz-4.0-chimaera-2022-7-ob-amd64.iso/download

//https://repo.aos.org.cn/iso/AOS-21.1-4-linux510-x86_64.iso

//https://sourceforge.net/projects/xos-workstation/files/latest/download

//https://github.com/instantOS/instantOS/releases/download/v7-beta/instantos_beta7.iso

//https://git.envs.net/WhichDoc/plagueOS_ISO/raw/branch/master/PlagueOS-R2.iso

//https://seilany.ir/predator-os/download/predator-os-v2.5-LTS.iso

//https://storage.rlxos.dev/releases/rlxos-stable-user-x86_64.iso

//https://sourceforge.net/projects/darkos-arch/files/latest/download

//https://github.com/dahliaOS/releases/releases/download/220222-x86_64/dahliaOS_220222_efi.zip

//https://github.com/ChimeraOS/install-media/releases/download/2023-02-15_432f20f/chimeraos-2023.02.15-x86_64.iso

//https://blackweb-security.org/download/BlackWeb-Trinity.3.10-amd64.iso

//https://github.com/regolith-linux/regolith-ubuntu-iso-builder/releases/download/release-release-hirsute-hirsute_standard-1.6.0/Regolith_1.6.0_hirsute.iso

//https://mazonos.com/releases/mazon-beta-1.5-livecd.iso

//https://sourceforge.net/projects/tux-linux/files/Tux%20Linux%2020.04%20-%20x64/MATE/tuxlinux2004-mate.iso/download

//https://sourceforge.net/projects/alien-os-nano/files/Alien-OS%20nano%20MX/Alien-OS_nano_MX.iso/download

//https://sourceforge.net/projects/tinypaw-linux/files/TinyPaw_v1.5.iso/download

//https://sourceforge.net/projects/hascodingos/files/latest/download

//https://xebian.org/download/xebian-unstable-amd64.hybrid.iso

//https://downloads.omnios.org/media/stable/omnios-r151044k.iso


