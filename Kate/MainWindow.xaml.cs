using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Kate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> Collection = new List<string>();
        
        public MainWindow()
        {
            InitializeComponent();
            
            ComboBox_client.Items.Add("VIP");
            ComboBox_client.Items.Add("Клиент");
            ComboBox_drink.Items.Add("Да");
            ComboBox_drink.Items.Add("Нет");
            ComboBox_balkon.Items.Add("Да");
            ComboBox_balkon.Items.Add("Нет");
            TextBox_pillow.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Collection.Count; i++)
            {
                FileStream fs = new FileStream("output.txt",FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(Collection[i]);
                sw.Close();
                fs.Close();
            }
            Collection.Clear();
            ListBox_Answer.Items.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (File.Exists("output.txt")) Process.Start("output.txt");
            else
            {
                FileStream fs = new FileStream("output.txt", FileMode.Create);
                fs.Close();
                Process.Start("output.txt");
            }

        }

        private void Button_Open_File_Click(object sender, RoutedEventArgs e)
        {
           
            if (File.Exists("input.txt"))
               Process.Start("input.txt");// File.Open("input.txt", FileMode.OpenOrCreate);
            else
            {
                FileStream fs = new FileStream("input.txt",FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("*Формат ввода данных: Количество человек(неотрицательное целое число),Тип клиента (Клиент/VIP),Наличие балкона,Количество подушек(неотрицательное целое число) ,Наличие алкоголя");
                sw.WriteLine("*Пробелы не ставить. Тип клиента пишется с большой буквы. Каждый новый запрос с новой строки");
                sw.WriteLine("*Для клиента не пишется параметр Количество подушек");
                sw.Close();
                fs.Close();
                File.Open("input.txt",FileMode.OpenOrCreate);
            }

        }

        private void Error(string s)
        {
            Error_window ew = new Error_window(s);
            ew.ShowDialog();
        }
        private bool check()
        {
            int temp;
            double temp_double;
            if (int.TryParse(TextBox_Block.Text, out temp) == false) { Error("Вы ввели нецелочисленный или отрицательный параметр для блока"); return false; }
            if (ComboBox_balkon.SelectedIndex == -1) { Error("Вы отметили не все поля"); return false; }
            if (double.TryParse(TextBox_pillow.Text, out temp_double) == false&&TextBox_pillow.IsEnabled==true) { Error("Вы ввели некорректный параметр для ширины"); return false; }
            if (ComboBox_drink.SelectedIndex==-1) { Error("Вы отметили не все поля"); return false; }
            if (TextBox_Block.Text == "" || (TextBox_pillow.Text == ""&&TextBox_pillow.IsEnabled==true)) { Error("Вы заполнили не все поля"); return false; }
            return true;
        }
        private void Button_Calculation_Click(object sender, RoutedEventArgs e)
        {
            Client f = null;
            VIP found = null;
            bool choice = true;
            if(check())
            {
                Person b = new Person(int.Parse(TextBox_Block.Text));
                if(ComboBox_client.SelectedValue=="Клиент") f = new Client(b,ComboBox_balkon.Text, ComboBox_drink.Text);
                else
                {
                     found = new VIP(ComboBox_balkon.Text, ComboBox_drink.Text,int.Parse(TextBox_pillow.Text), b);
                    choice=false;
                }
            }
            TextBox_Block.Clear();
            ComboBox_balkon.SelectedIndex = -1;
            ComboBox_drink.SelectedIndex = -1;
            TextBox_pillow.Clear();
            string s;
            if(choice) s= f.Show();else s=found.Show();
            ListBox_Answer.Items.Add(s);
            Collection.Add(s);
            
        }

        private void ComboBox_Object_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_client.SelectedValue == "Клиент") TextBox_pillow.IsEnabled = false;
            if (ComboBox_client.SelectedValue != "Клиент") TextBox_pillow.IsEnabled = true;
        }

        private void Button_File_Calculation_Click(object sender, RoutedEventArgs e)
        {

            FileStream fs = new FileStream("input.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string s;
            ListBox_Answer.Items.Clear();
            Collection.Clear();
            while (!sr.EndOfStream)
            {
                s = sr.ReadLine();
                if (s[0] == '*') continue;
                string[] temp = s.Split(',');
                if (temp.Length != 4 && temp.Length != 5) continue;
                if (temp[1] == "Клиент")
                {
                    int block;
                    double height, length;
                    if (!int.TryParse(temp[0], out block) || !double.TryParse(temp[2], out length) || !double.TryParse(temp[3], out height)) { Error("Файл поврежден"); break; }
                    Client f = new Client(new Person(int.Parse(temp[0])), temp[2], temp[3]);
                    ListBox_Answer.Items.Add(f.Show());
                    Collection.Add(f.Show());

                }
                else
                    if (temp[1] == "VIP")
                    {

                        int block;
                        double height, length, width;
                        if (!int.TryParse(temp[0], out block) || !double.TryParse(temp[2], out length) || !double.TryParse(temp[3], out width) || !double.TryParse(temp[4], out height)) { Error("Файл поврежден"); break; }
                        VIP f = new VIP(temp[2], temp[3], int.Parse(temp[4]), new Person(int.Parse(temp[0])));
                        ListBox_Answer.Items.Add(f.Show());
                        Collection.Add(f.Show());



                    }
                    else
                        break;
            }
            sr.Close();
            fs.Close();
        }
    }
}
