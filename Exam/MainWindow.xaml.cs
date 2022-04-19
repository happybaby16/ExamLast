using Exam.Model;
using Exam.pages;
using Exam.ViewModel;
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

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 


    public static class DataBase
    {
        public static Entities Tables;
    }

    public static class PageLoader
    {
        public static Frame Loader { get; set; }
    }

    public partial class MainWindow : Window
    {
        ViewModelMarket VMM;
        public MainWindow()
        {
            InitializeComponent();
            DataBase.Tables = new Entities();
            VMM = new ViewModelMarket();
            PageLoader.Loader = pageLoader;
            PageLoader.Loader.Navigate(new PageMarketList(VMM));
        }
    }
}
