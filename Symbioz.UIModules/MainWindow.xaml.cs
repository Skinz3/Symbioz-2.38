using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Symbioz.UIModules
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "Select Dofus 2.0 Root path (/app/)";
         //   dialog.ShowDialog();
            dialog.SelectedPath = @"C:\Users\Skinz\Desktop\Symbioz 2.38\app";

            if (dialog.SelectedPath != string.Empty)
            {
                Editor editorWindow = new Editor(dialog.SelectedPath, moduleNameTb.Text);
                editorWindow.Show();
                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Please specify dofus root path.", "Error");
            }
        }
    }
}