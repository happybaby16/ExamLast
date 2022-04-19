using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountDLL
{
    public static class Discount
    {
        public static double Get(int countBook, double priceShop)
        {
            double discount = 0;
            try
            {
                if (countBook >= 3 && countBook < 5)
                {
                    discount += 0.05;
                }
                else if (countBook >= 5 && countBook < 10)
                {
                    discount += 0.10;
                }
                else if (countBook >= 10)
                {
                    discount += 0.15;
                }
                discount += Math.Truncate(priceShop / 500) * 0.01;
            }
            catch
            { }
            return discount;
        }
    }
}
