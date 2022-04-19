using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    partial class Book
    {
        public string StringDesc
        {
            get => $"Описание: {Description}";
        }

        public string ActualImage
        {
            get
            {
                return Environment.CurrentDirectory+ "\\" + Image; ;
            }
        }
    }
}
