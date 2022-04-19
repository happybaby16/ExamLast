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

namespace Exam.pages
{
    /// <summary>
    /// Логика взаимодействия для PageMarketList.xaml
    /// </summary>
    public partial class PageMarketList : Page
    {
        ViewModelMarket VMM;
        public PageMarketList(ViewModelMarket VMM)
        {
            InitializeComponent();
            this.VMM = VMM;
            DataContext = VMM;
            CommandBindings.Add(VMM.AddBookToBagBinding);
        }

        private void Button_ToBag(object sender, RoutedEventArgs e)
        {
            PageLoader.Loader.Navigate(new PageMarketBag(VMM));
        }
    }
}
