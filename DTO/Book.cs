using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL_Tan.DTO
{
    class Book
    {
        private String bookId;
        private String name;
        private String category;
        private double price;
        private int stock;
        private String author;

        public string BookId { get => bookId; set => bookId = value; }
        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }
        public double Price { get => price; set => price = value; }
        public int Stock { get => stock; set => stock = value; }
        public string Author { get => author; set => author = value; }

        public Book(String bookId, String name, String category, double price, int stock, String author)
        {
            this.BookId = bookId;
            this.Name = name;
            this.Category = category;
            this.Price = price;
            this.Stock = stock;
            this.Author = author;
        }

        
    }
}
