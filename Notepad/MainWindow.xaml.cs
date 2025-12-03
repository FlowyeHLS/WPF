using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Microsoft.Win32;

using IOPath = System.IO.Path;
using IOFile = System.IO.File;

namespace Notepad
{
    public partial class MainWindow : Window
    {
        private string currentFileName = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            txtEditor.Clear();
            currentFileName = string.Empty;
            Title = "Блокнот";
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    txtEditor.Text = IOFile.ReadAllText(dialog.FileName);
                    currentFileName = dialog.FileName;
                    Title = "Блокнот - " + IOPath.GetFileName(currentFileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при открытии файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(currentFileName))
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                dialog.DefaultExt = "txt";

                if (dialog.ShowDialog() == true)
                {
                    currentFileName = dialog.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                IOFile.WriteAllText(currentFileName, txtEditor.Text);
                Title = "Блокнот - " + IOPath.GetFileName(currentFileName);
                MessageBox.Show("Файл сохранен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}