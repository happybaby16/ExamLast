using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class BookBag : IDisposable
    {
        public Book BagBook { get; set; }
        public int BookCount { get; set; }
        public int FromMarket { get; set; }
        public int FromWarehouse { get; set; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        //Задается через ViewModel
        public double Discount { get; set; }

        public double PriceWithoutDiscount
        {
            get => Convert.ToDouble(BagBook.Price) * BookCount;
        }
        public double PriceWithDiscount
        {
            get => Math.Round(PriceWithoutDiscount * (1 - Discount), 2);
        }



    }
}
