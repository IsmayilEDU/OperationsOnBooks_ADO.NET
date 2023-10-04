using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsOnBooks
{
    public class Book
    {
        public Book(int iD, string? name, int pages)
        {
            ID = iD;
            Name = name;
            Pages = pages;
        }

        public int ID { get; set; }
        public string? Name { get; set; }
        public int Pages { get; set; }
    }
}
