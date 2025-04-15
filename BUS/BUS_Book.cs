
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using PBL_Tan.DAL;
using PBL_Tan.DTO;

namespace PBL_Tan.BUS
{
    class BUS_Book
    {
        #region khoi tao
        private static BUS_Book instance;

        internal static BUS_Book Instance { 
            get
            {
                if(instance == null)
                {
                    instance = new BUS_Book();
                }
                return instance;
            } 
            set => instance = value; 
        }
        #endregion

        #region methode
        List<Book> books= new List<Book>();
        public List<Book> GetAllCustomer()
        {
            books = DAL_Book.Instance.getBook();
            return books;
        }
        public int CountBooks()
        {
            return DAL_Book.Instance.CountBook();
        }
        public void AddBook(string bookId, string name, string category, double price, int stock, string author)
        {
            DAL_Book.Instance.AddBook(bookId, name, category, price, stock, author);
        }
        public void UpdateBook(Book book, int index)
        {
            DAL_Book.Instance.UpdateBook(book, index);
        }
        public void DeleteBook(int index)
        {
            DAL_Book.Instance.DeleteBook(index);
        }
        #endregion
    }
}
