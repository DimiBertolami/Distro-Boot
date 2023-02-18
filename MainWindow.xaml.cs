﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// </summary>
    public partial class MainWindow : Window
    {
        int hWnd = 0;
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void BootHidden(object sender, MouseButtonEventArgs e)
        {
            Process ps = Global.ps;
            IntPtr Handle = ps.Handle;
            Global.Handle = (int)Handle;
            ps.StandardInput.WriteLine("pushd c:\\pe__data");
            string fileName = IMGs.SelectedValue.ToString();


            if (fileName.Contains(".IMG") || fileName.Contains(".img"))
            {
                ps.StandardInput.WriteLine("%q%  -m 10000 -drive file=" + (char)34 + fileName + (char)34 + ",format=raw,index=0,media=disk -vga virtio -m 10G -name " + fileName + " -no-reboot");
            }
            if (fileName.Contains(".ISO") || fileName.Contains(".iso"))
            {
                ps.StandardInput.WriteLine("%q% -cdrom " + (char)34 + fileName + (char)34 + " -m 10G");
            }
        }

        private void FindImages(object sender, RoutedEventArgs e)
        {
            Process ps = new();
            Global.ps = ps;
            ps.StartInfo.RedirectStandardInput = true;
            ps.StartInfo.CreateNoWindow = true;
            ps.StartInfo.FileName = "cmd.exe";
            ps.StartInfo.UseShellExecute = false;
            ps.Start();

            IntPtr Handle = ps.Handle;
            Global.Handle = (int)Handle;

            OpenFileDialog dlg = new();
            dlg.InitialDirectory = "c:\\pe__data\\";
            dlg.ShowDialog();
            string fileName = dlg.FileName;
            if (!IMGs.Items.Contains(fileName))
            {
                IMGs.Items.Add(fileName);
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
                }
            }
        }
    }
}