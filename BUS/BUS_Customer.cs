using PBL_Tan.DAL;
using PBL_Tan.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL_Tan.BUS
{
    class BUS_Customer
    {
        #region Singleton
        private static BUS_Customer instance;
        private BUS_Customer() { }
        public static BUS_Customer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUS_Customer();
                }
                return instance;
            }
        }
        #endregion
        #region Customer Operations
        // lấy toàn bộ các dòng dữ liệu
        public List<User> GetAllCustomer()
        {
            return DAL_Customer.Instance.GetCustomers();
        }

        // thêm một dòng dữ liệu
        public void AddCustomer(User customer)
        {
            DAL_Customer.Instance.AddCustomer(customer);
        }

        // đếm số lượng dòng dữ liệu
        public int CountCustomer()
        {
            return DAL_Customer.Instance.CountCustomer();
        }
        public void UpdateCustomer(User customer, int index)
        {
            DAL_Customer.Instance.UpdateCustomer(customer, index);
        }
        public void DeleteCustomer(int index)
        {
            DAL_Customer.Instance.DeleteCustomer(index);
        }
        #endregion

    }
}
