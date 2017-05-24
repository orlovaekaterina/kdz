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

namespace Kate
{
    /// <summary>
    /// Логика взаимодействия для Error_window.xaml
    /// </summary>
    public partial class Error_window : Window
    {
        public Error_window(string s)
        {
            InitializeComponent();
            Label_text.Content = s;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
