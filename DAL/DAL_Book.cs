using PBL_Tan.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL_Tan.DAL
{
    class DAL_Book
    {
        #region khoi tao
        private static DAL_Book instance;

        internal static DAL_Book Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_Book();
                }
                return instance;
            }
            set => instance = value;
        }
        private DAL_Book() { }
        #endregion

        #region filePath
        private string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\PBL_Tan\Data\Book.csv";
        #endregion
        List<Book> books = new List<Book>();
        public List<Book> getBook()
        {
            try
            {
                books.Clear();
                List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
                if (data.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Không có dữ liệu trong file CSV.");
                }
                foreach (var row in data)
                {
                    if (row.Length == 6)
                    {
                        if (double.TryParse(row[3], out double price) && int.TryParse(row[4], out int stock))
                        {
                            books.Add(new Book(row[0], row[1], row[2], price, stock, row[5]));
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine($"Dòng có dữ liệu không hợp lệ: {string.Join(",", row)}");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Dòng không đủ cột: {string.Join(",", row)}");
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Tổng số khách hàng: {books.Count}");
                return books;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đọc CSV: {ex.Message}");
                throw new Exception($"Lỗi khi đọc dữ liệu khách hàng: {ex.Message}");
            }
        }
        public void AddBook(string bookId, string name, string category, double price, int stock, string author)
        {
            try
            {
                Book book = new Book(bookId, name, category, price, stock, author);
                books.Add(book);

                // Convert the list of books to a list of string arrays for writing to CSV  
                List<string[]> bookData = books.Select(b => new string[]
                {
                   b.BookId,
                   b.Name,
                   b.Category,
                   b.Price.ToString(),
                   b.Stock.ToString(),
                   b.Author
                }).ToList();

                DataProvider.Instance.Write_CSV(filePath, bookData);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi thêm sách: {ex.Message}");
            }
        }
        public void UpdateBook(Book book, int index)
        {
            // Cập nhật thông tin sách  
            books[index] = book;

            // Ghi lại toàn bộ danh sách vào file CSV  
            List<string[]> allData = new List<string[]>();
            allData.AddRange(books.Select(c => new string[] { c.BookId, c.Name, c.Category, c.Price.ToString(), c.Stock.ToString(), c.Author }).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }
        public void Update_Book_ID(List<Book> books, int index)
        {
            for (int i = index; i < books.Count; i++)
            {
                books[i].BookId = "B" + (i + 1).ToString("D3");
            }
        }
        public void DeleteBook(int index)
        {
            books.RemoveAt(index);
            Update_Book_ID(books, index);
            List<string[]> allData = new List<string[]>();
            allData.AddRange(books.Select(c => new string[] { c.BookId, c.Name, c.Category, c.Price.ToString(), c.Stock.ToString(), c.Author }).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }
        public int CountBook()
        {
            int count = 0;
            try
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        // Kiểm tra dòng không rỗng và không chỉ chứa khoảng trắng
                        if (!string.IsNullOrWhiteSpace(line))
                        {
                            count++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File CSV không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đọc file CSV: " + ex.Message);
            }
            return count;
        }
    }
}
