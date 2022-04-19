using Exam.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Exam.ViewModel
{
    public class ViewModelMarket:INotifyPropertyChanged
    {
        ObservableCollection<Book> books = new ObservableCollection<Book>(DataBase.Tables.Book.ToList());

        ObservableCollection<BookBag> bag = new ObservableCollection<BookBag>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Book> BooksMarket
        {
            get => books;
            set => books = value;
        }

        public ObservableCollection<BookBag> BooksBag
        {
            get => bag;
            set => bag = value;
        }




        double discount = 0;
        public double GetDiscount
        {
            get
            {
                Discount();
                PropertyChanged(this, new PropertyChangedEventArgs("GetDiscountString"));
                PropertyChanged(this, new PropertyChangedEventArgs("IsHaveDiscount"));
                PropertyChanged(this, new PropertyChangedEventArgs("GetTotalPriceWithoutDiscount"));
                return discount;
            }
        }
        public string GetDiscountString
        {
            get => $"Размер скидки: {Convert.ToString(discount * 100)}%";
        }
        public bool IsHaveDiscount
        {
            get
            {
                if (discount > 0) return true;
                else return false;
            }
        }

        int countBookInBag = 0;
        public int CountBooksInBag
        {
            get
            {
                countBookInBag = 0;
                foreach (BookBag bag in bag)
                {
                    countBookInBag += bag.BookCount;
                }
                PropertyChanged(this, new PropertyChangedEventArgs("GetDiscount"));
                return countBookInBag;
            }
        }

        double totalPriceWithoutDiscount = 0;
        public double GetTotalPriceWithoutDiscount
        {
            get
            {
                double totalPriceWithoutDiscount = 0;
                foreach (BookBag bag in bag)
                {
                    totalPriceWithoutDiscount += bag.PriceWithoutDiscount;
                }
                PropertyChanged(this, new PropertyChangedEventArgs("GetTotalPriceWithDiscount"));
                return totalPriceWithoutDiscount;
            }
        }

        
        public double GetTotalPriceWithDiscount
        {
            get => Math.Round(totalPriceWithoutDiscount * (1 - discount), 2);
        }

        public string GetTotalPriceWithoutDiscountString
        {
            get => GetTotalPriceWithoutDiscount + " руб.";
        }
        public string GetTotalPriceWithDiscountString
        {
            get => GetTotalPriceWithDiscount + " руб.";
        }

        private void Discount()
        {
            discount = DiscountDLL.Discount.Get(countBookInBag, totalPriceWithoutDiscount);
        }

        Book selectedBook;
        public Book SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsCanAddMainPage"));
            }
        }

        public RoutedCommand AddBookToBagCommand { get; set; } = new RoutedCommand();
        public CommandBinding AddBookToBagBinding;
        public void AddBookToBag(object sender, EventArgs e)
        {
            if (books.Single(x => x == SelectedBook).CountMarket > 0 || books.Single(x => x == SelectedBook).CountWarehouse > 0)
            {
                if (!bag.Any(x => x.BagBook == SelectedBook))
                {
                    BookBag createdOrder = new BookBag() { BagBook = SelectedBook };
                    if (SelectedBook.CountMarket != createdOrder.FromMarket && SelectedBook.CountMarket > 0)
                    {
                        createdOrder.BookCount++;
                        createdOrder.FromMarket++;
                    }
                    else if (SelectedBook.CountWarehouse != createdOrder.FromWarehouse && SelectedBook.CountWarehouse > 0)
                    {
                        createdOrder.BookCount++;
                        createdOrder.FromWarehouse++;
                    }
                    bag.Add(createdOrder);

                }
                else
                {
                    BookBag selectedBook = bag.Single(x => x.BagBook == SelectedBook);
                    if (SelectedBook.CountMarket != selectedBook.FromMarket && SelectedBook.CountMarket > 0)
                    {
                        selectedBook.BookCount++;
                        selectedBook.FromMarket++;
                    }
                    else if (SelectedBook.CountWarehouse != selectedBook.FromWarehouse && SelectedBook.CountWarehouse > 0)
                    {
                        selectedBook.BookCount++;
                        selectedBook.FromWarehouse++;
                    }

                }
            }
            PropertyChanged(this, new PropertyChangedEventArgs("CountBooksInBag"));
        }


        BookBag selectedBookBag;
        public BookBag SelectedBookBag
        {
            get => selectedBookBag;
            set
            {
                selectedBookBag = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsCanAdd"));
            }
        }

        public RoutedCommand AddBookFromBagCommand { get; set; } = new RoutedCommand();
        public CommandBinding AddBookFromBagBinding;
        public void AddBookFromBookBagToBag(object sender, EventArgs e)
        {
            if (books.Single(x => x == SelectedBookBag.BagBook).CountMarket > 0 || books.Single(x => x == SelectedBookBag.BagBook).CountWarehouse > 0)
            {
                if (!bag.Any(x => x.BagBook == SelectedBookBag.BagBook))
                {
                    BookBag createdOrder = new BookBag() { BagBook = SelectedBookBag.BagBook };
                    if (SelectedBookBag.BagBook.CountMarket != createdOrder.FromMarket && SelectedBookBag.BagBook.CountMarket > 0)
                    {
                        createdOrder.BookCount++;
                        createdOrder.FromMarket++;
                    }
                    else if (SelectedBookBag.BagBook.CountWarehouse != createdOrder.FromWarehouse && SelectedBookBag.BagBook.CountWarehouse > 0)
                    {
                        createdOrder.BookCount++;
                        createdOrder.FromWarehouse++;
                    }
                    bag.Add(createdOrder);

                }
                else
                {
                    BookBag selectedBook = bag.Single(x => x.BagBook == SelectedBookBag.BagBook);
                    if (SelectedBookBag.BagBook.CountMarket != selectedBook.FromMarket && SelectedBookBag.BagBook.CountMarket > 0)
                    {
                        selectedBook.BookCount++;
                        selectedBook.FromMarket++;
                    }
                    else if (SelectedBookBag.BagBook.CountWarehouse != selectedBook.FromWarehouse && SelectedBookBag.BagBook.CountWarehouse > 0)
                    {
                        selectedBook.BookCount++;
                        selectedBook.FromWarehouse++;
                    }

                }
            }
            bag = new ObservableCollection<BookBag>(bag);
            PropertyChanged(this, new PropertyChangedEventArgs("GetDiscount"));
            PropertyChanged(this, new PropertyChangedEventArgs("IsCanAdd"));
            PropertyChanged(this, new PropertyChangedEventArgs("BooksBag"));
            PropertyChanged(this, new PropertyChangedEventArgs("CountBooksInBag"));
        }

        public RoutedCommand RemoveBookFromBagCommand { get; set; } = new RoutedCommand();
        public CommandBinding RemoveBookFromBagBinding;
        public void RemoveBookFromBookBagFromBag(object sender, EventArgs e)
        {
            try
            {
                if (bag.Any(x => x.BagBook == SelectedBookBag.BagBook) && books.Single(x => x == SelectedBookBag.BagBook).CountMarket > 0 || books.Single(x => x == SelectedBook).CountWarehouse > 0)
                {
                    BookBag selectedBook = bag.Single(x => x.BagBook == SelectedBookBag.BagBook);
                    if (selectedBook.FromWarehouse != 0)
                    {
                        selectedBook.BookCount--;
                        selectedBook.FromWarehouse--;
                    }
                    else if (selectedBook.FromMarket != 0)
                    {
                        selectedBook.BookCount--;
                        selectedBook.FromMarket--;
                    }
                    if (selectedBook.BookCount == 0)
                    {
                        bag.Remove(selectedBook);
                    }
                }
            }
            catch { }
            bag = new ObservableCollection<BookBag>(bag);
            PropertyChanged(this, new PropertyChangedEventArgs("BooksBag"));
            PropertyChanged(this, new PropertyChangedEventArgs("IsCanAdd"));
            PropertyChanged(this, new PropertyChangedEventArgs("GetDiscount"));
            PropertyChanged(this, new PropertyChangedEventArgs("CountBooksInBag"));
        }





        public RoutedCommand RemoveAllCommand { get; set; } = new RoutedCommand();
        public CommandBinding RemoveAllBinding;
        public void RemoveAll (object sender, EventArgs e)
        {
            bag = new ObservableCollection<BookBag>();
            PropertyChanged(this, new PropertyChangedEventArgs("BooksBag"));
            PropertyChanged(this, new PropertyChangedEventArgs("IsCanAdd"));
            PropertyChanged(this, new PropertyChangedEventArgs("GetDiscount"));
            PropertyChanged(this, new PropertyChangedEventArgs("CountBooksInBag"));
        }





        public RoutedCommand BuyBooksCommand { get; set; } = new RoutedCommand();
        public CommandBinding BuyBooksBinding;
        public void BuyBooks(object sender, EventArgs e)
        {
            double totalPrice=0;

            foreach (var item in BooksBag)
            {
                totalPrice += item.PriceWithoutDiscount;
            }


            bool isHaveWarehouse = false;

            Order newOrder = new Order();
            newOrder.CountBook = CountBooksInBag;
            newOrder.TotalPrice = totalPrice;
            newOrder.Discount = discount;
            foreach (BookBag item in BooksBag)
            {
                if (item.FromWarehouse > 0)
                {
                    isHaveWarehouse = true;
                    break;
                }
            }
            if (isHaveWarehouse)
            {
                newOrder.WarehouseDate = DateTime.Now.AddDays(3);
            }
            newOrder.RegistrationDate = DateTime.Now;
            newOrder.BookingDate = DateTime.Now.AddDays(7);
            DataBase.Tables.Order.Add(newOrder);
            DataBase.Tables.SaveChanges();
            foreach (BookBag item in BooksBag)
            {
                for (int i = 0; i < item.BookCount; i++)
                {
                    BookOrder newBook = new BookOrder();
                    newBook.ID_Order = newOrder.ID;
                    newBook.ID_Book = item.BagBook.ID;
                    DataBase.Tables.BookOrder.Add(newBook);
                }
                
            }
            DataBase.Tables.SaveChanges();

            if (isHaveWarehouse)
            {
                MessageBox.Show($"Номер заказа: {newOrder.ID}. Поскольку некоторые книги есть только на складе, то забрать полностью заказ можно будет {newOrder.WarehouseDate}. Бронирование заказа закончится {newOrder.BookingDate}");
            }
            else
            {
                MessageBox.Show($"Номер заказа: {newOrder.ID}. Книги можно забрать на кассе {newOrder.RegistrationDate}");
            }
        }



        public bool IsCanAddInBag
        {
            get
            {
                try
                {
                    if (SelectedBookBag == null || SelectedBookBag.BookCount == BooksMarket.Single(x => x == SelectedBookBag.BagBook).CountWarehouse + BooksMarket.Single(x => x == SelectedBookBag.BagBook).CountMarket) return false;
                    else return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsCanAddMainPage
        {
            get
            {
                try
                {
                    if (SelectedBook == null || (SelectedBook.CountMarket == 0 && SelectedBook.CountWarehouse == 0)) return false;
                    else return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public ViewModelMarket()
        {
            AddBookToBagBinding = new CommandBinding(AddBookToBagCommand);
            AddBookToBagBinding.Executed += AddBookToBag;

            RemoveBookFromBagBinding = new CommandBinding(RemoveBookFromBagCommand);
            RemoveBookFromBagBinding.Executed += RemoveBookFromBookBagFromBag;

            AddBookFromBagBinding = new CommandBinding(AddBookFromBagCommand);
            AddBookFromBagBinding.Executed += AddBookFromBookBagToBag;

            RemoveAllBinding = new CommandBinding(RemoveAllCommand);
            RemoveAllBinding.Executed += RemoveAll;

            BuyBooksBinding = new CommandBinding(BuyBooksCommand);
            BuyBooksBinding.Executed += BuyBooks;
        }
    }
}
