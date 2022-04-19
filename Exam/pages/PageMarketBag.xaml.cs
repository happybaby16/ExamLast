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
    /// Логика взаимодействия для PageMarketBag.xaml
    /// </summary>
    public partial class PageMarketBag : Page
    {
        ViewModelMarket VMM;
        public PageMarketBag(ViewModelMarket VMM)
        {
            InitializeComponent();
            this.VMM = VMM;
            DataContext = VMM;
            CommandBindings.Add(VMM.RemoveBookFromBagBinding);
            CommandBindings.Add(VMM.AddBookFromBagBinding);
            CommandBindings.Add(VMM.RemoveAllBinding);
            CommandBindings.Add(VMM.BuyBooksBinding);
            
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            PageLoader.Loader.GoBack();
        }
    }
}
