using System;
using System.Collections.Generic;
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
using System.IO;

using iTextSharp.text.pdf;
using iTextSharp.text.pdf.security;
using iTextSharp.text.io;

/*
using PdfSharp;
using PdfSharp.Charting;
using PdfSharp.Drawing;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Security;
using PdfSharp.Pdf.IO;
*/





namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.IO.Directory.CreateDirectory("C:/PDF/");

        }

        private void Button_Copy(object sender, RoutedEventArgs e)//PROTECT PDF ALLOWING IMPRESION, EDITION AND COPY/PASTE
        {
            string Password = "SecurePass123456";

            DirectoryInfo dir = new DirectoryInfo("C:/PDF/");            

            if (dir.GetFiles("*.pdf").Length ==0)
            {
                MessageBox.Show("There are no files in the default directory", "No files", MessageBoxButton.OK, MessageBoxImage.Warning);

                tbx.Background = Brushes.OrangeRed;
                tbx.Text = "No Files Found";
            }
            else
            {
                tbx.Background = Brushes.White;
                tbx.Text = "Protecting....";

                foreach (FileInfo file in dir.GetFiles("*.pdf"))
                {
                    try
                    { 

                        string InputFile = System.IO.Path.Combine("C:/PDF/", file.Name);
                        string OutputFile = System.IO.Path.Combine("C:/PDF/", "@" + file.Name);
                        using (Stream input = new FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (Stream output = new FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                PdfReader.unethicalreading = true;
                                PdfReader reader = new PdfReader(input);
                                PdfEncryptor.Encrypt(reader, output, true, null, Password, PdfWriter.ALLOW_MODIFY_CONTENTS | PdfWriter.AllowFillIn | PdfWriter.AllowCopy | PdfWriter.AllowPrinting);
                            }
                        }
                        file.Delete();
                        File.Move(@"C:\PDF\@" + file.Name, @"C:\PDF\" + file.Name);

                        tbx.Text = "Allow Copy";
                        tbx.Background = Brushes.ForestGreen;
                    }
                    catch (Exception)
                    {
                        tbx.Text = "Error in: " + file.Name;
                        tbx.Background = Brushes.IndianRed;                                                
                    }
                }

            }            
        }

        private void Button_Full(object sender, RoutedEventArgs e)//PROTECT PDF ONLY ALLOWING IMPRESION
        {
            string Password = "SecurePass123456";

            DirectoryInfo dir = new DirectoryInfo("C:/PDF/");

            if(dir.GetFiles("*.pdf").Length ==0)
            {
                MessageBox.Show("There are no files in the default directory", "No Files", MessageBoxButton.OK, MessageBoxImage.Warning);
                tbx.Background = Brushes.OrangeRed;
                tbx.Text = "No Files Found";
            }
            else
            {
                tbx.Background = Brushes.White;
                tbx.Text = "Protecting....";

                foreach (FileInfo file in dir.GetFiles("*.pdf"))
                {
                    try
                    {     
                        string InputFile = System.IO.Path.Combine("C:/PDF/", file.Name);
                        string OutputFile = System.IO.Path.Combine("C:/PDF/", "@"+file.Name);
                        using (Stream input = new FileStream(InputFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (Stream output = new FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                PdfReader.unethicalreading = true;
                                PdfReader reader = new PdfReader(input);                                
                                PdfEncryptor.Encrypt(reader, output, true, null, Password, PdfWriter.AllowPrinting);                                 

                            }
                        }

                        file.Delete();
                        File.Move(@"C:\PDF\@"+file.Name, @"C:\PDF\"+file.Name);

                        tbx.Text = "Full Protected";
                        tbx.Background = Brushes.ForestGreen;
                    }
                    catch (Exception ex)
                    {
                        tbx.Text = "Error in: " + file.Name + ex;
                        tbx.Background = Brushes.IndianRed;

                    }
                }
            }                      
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            tbx.Background = Brushes.White;
            tbx.Text = string.Empty;
        }
    }    
}
