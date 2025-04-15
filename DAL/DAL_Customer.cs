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
    class DAL_Customer
    {
        #region khoi tao
        private static DAL_Customer instance;

        internal static DAL_Customer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DAL_Customer();
                }
                return instance;
            }
            set => instance = value;
        }
        #endregion

        #region file path
        private static string filePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\PBL_Tan\Data\Customer.csv";
        #endregion

        #region method
        public List<User> customers = new List<User>();
        // truy xuất dữ liệu
        public List<User> GetCustomers()
        {
            try
            {
                customers.Clear();
                List<string[]> data = DataProvider.Instance.ReadCsv(filePath);
                if (data.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Không có dữ liệu trong file CSV.");
                }
                foreach (var row in data)
                {
                    if (row.Length == 6)
                    {
                        customers.Add(new User(row[0], row[1], row[2], row[3], row[4], row[5]));
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"Dòng không đủ cột: {string.Join(",", row)}");
                    }
                }
                System.Diagnostics.Debug.WriteLine($"Tổng số khách hàng: {customers.Count}");
                return customers;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi đọc CSV: {ex.Message}");
                throw new Exception($"Lỗi khi đọc dữ liệu khách hàng: {ex.Message}");
            }
        }
        // Updated AddCustomer method to fix CS1503 error
        public void AddCustomer(User customer)
        {
            customers.Add(customer);
            List<string[]> row = new List<string[]>
            {
                new string[] { customer.UserId, customer.Email, customer.Phone, customer.Password }
            };
            DataProvider.Instance.Append_CSV(filePath, row);
        }

        public int CountCustomer()
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
        public void Update_User_ID(List<User> customers, int index)
        {
            // Cập nhật UserId cho tất cả khách hàng
            for (int i = index; i < customers.Count; i++)
            {
                customers[i].UserId = "U" + (i + 1).ToString("D3");
            }
        }
        public void UpdateCustomer(User customer, int index)
        {
            // Cập nhật thông tin khách hàng
            customers[index] = customer;

            // Ghi lại toàn bộ danh sách vào file CSV
            List<string[]> allData = new List<string[]>();
            allData.AddRange(customers.Select(c => new string[] { c.UserId, c.UserName, c.Name, c.Email, c.Phone, c.Password }).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }
        public void DeleteCustomer(int index)
        {
            // Kiểm tra xem index có hợp lệ không
            if (index < 0 || index >= customers.Count)
            {
                throw new ArgumentOutOfRangeException("Index is out of range.");
            }

            // Xóa khách hàng tại vị trí index
            customers.RemoveAt(index);

            // Cập nhật lại UserId từ vị trí index trở đi
            Update_User_ID(customers, index);

            // Ghi lại toàn bộ danh sách vào file CSV
            List<string[]> allData = new List<string[]>();
            allData.AddRange(customers.Select(c => new string[] { c.UserId, c.UserName, c.Name, c.Email, c.Phone, c.Password }).ToList());
            DataProvider.Instance.Write_CSV(filePath, allData);
        }
        #endregion
    }
}
