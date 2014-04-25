using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace HraZivota
{
    /// <summary>
    /// Interaction logic for Napoveda.xaml
    /// </summary>
    public partial class Napoveda : Window
    {
        public Napoveda()
        {
            InitializeComponent();
            nacteniNapovedy();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void nacteniNapovedy()
        {
            string s;
            try
            {
                using (StreamReader sr = new StreamReader(@"napoveda", Encoding.Default))
                {
                    s = sr.ReadToEnd();
                    textBlock1.Text = s;

                    /*while ((s = sr.ReadLine()) != null)
                    {
                        //s = sr.ReadLine();
                        listView1.Items.Add(s);
                    }*/
                }
            }
            catch (FileNotFoundException)
            {
                //listView1.Items.Add("Nebyl nalezen soubor s nápovědou");
                //MessageBox.Show("Nebyl nalezen soubor s nápovědou.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
                //this.Close();
            }
        }
    }
}
