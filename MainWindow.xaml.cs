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

namespace SampleProject
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string enteredName = txtName.Text;
            string idVal = id.Text;


            if (string.IsNullOrWhiteSpace(enteredName) || string.IsNullOrWhiteSpace(idVal))
            {
                MessageBox.Show("Please Enter Name and ID");
            }
            else
            {
                //string filepath = @"E:\Write22.txt";

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = @"E:\";

                if (openFileDialog.ShowDialog() == true)
                {
                    string filepath = openFileDialog.FileName;

                    if (File.Exists(filepath))
                    {
                        using (StreamWriter writer = File.AppendText(filepath))
                        {
                            writer.WriteLine($"Name: {enteredName}, ID: {idVal}" + " - Checked In" + " " + DateTime.Now);
                        }
                        MessageBox.Show("Check-In Successful. Enjoy Your Stay.");
                    }
                    else
                    {
                        MessageBox.Show("The selected file does not exist.");
                    }
                }
                txtName.Clear();
                id.Clear();
            }
        }

        private void ValuesRead_Button(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
                       
            openFileDialog.InitialDirectory = @"E:\";

            if (openFileDialog.ShowDialog() == true)
            {
                string filepath = openFileDialog.FileName;

                if (File.Exists(filepath))
                {
                    using (StreamReader Read = File.OpenText(filepath))
                    {
                        DataRead.Text = Read.ReadToEnd();
                    }
                }
                else
                {
                    MessageBox.Show("The selected file does not exist.");
                }
            }
        }

       
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            string enteredName = txtName.Text;
            string idVal = id.Text;
            try
            {
                if (string.IsNullOrWhiteSpace(enteredName) || string.IsNullOrWhiteSpace(idVal))
                {
                    MessageBox.Show("Please Enter Name and ID");
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.InitialDirectory = @"E:\";

                    if (openFileDialog.ShowDialog() == true)
                    {
                        string filepath = openFileDialog.FileName;
                        string[] lines = File.ReadAllLines(filepath);
                        bool found = false;

                        for (int i = 0; i < lines.Length; i++)
                        {
                            if (lines[i].Contains($"Name: {enteredName}, ID: {idVal}"))
                            {
                                lines[i] += $" - Checkout Successful {DateTime.Now}";
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            MessageBox.Show("No matching entry found for checkout.");
                        }
                        else
                        {
                            File.WriteAllLines(filepath, lines);
                            MessageBox.Show("Checkout Successful. Thank You. Visit Again.");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                id.Clear();
                txtName.Clear();
            }       
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

