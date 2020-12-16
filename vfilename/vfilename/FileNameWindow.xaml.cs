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
using System.Windows.Shapes;

namespace vfilename
{



    /// <summary>
    /// FileNameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileNameWindow : Window
    {
        public FileNameWindow()
        {
            InitializeComponent();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

            base.OnClosing(e);

                e.Cancel = true;
                this.Hide();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(((TextBox)sender).Text=="用户名")
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "说明")
            {
                ((TextBox)sender).Text = "";
            }
        }
        public void InitShow()
        {
            this.DateTimeTextBox.Text = DateTime.Now.ToString("yyyy.MM.dd@HH.mm.ss");
            this.DescriptionTextBox.Text = "说明";
            this.Show();
            this.CompressButton.Focus();
        }

        private void UserNameTextBox_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void DescriptionTextBox_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            ChildWindows.Go("-"+this.UserNameTextBox.Text+"-"+this.DateTimeTextBox.Text+"-"+this.DescriptionTextBox.Text);
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConfigTxt.Write("username", ((TextBox)sender).Text, "用户名");
        }

        private void DescriptionTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                CompressButton_Click(null, null);
            }
        }
    }


}
