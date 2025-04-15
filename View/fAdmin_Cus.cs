using PBL_Tan.BUS;
using PBL_Tan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL_Tan.View
{
    public partial class fAdmin_Cus: Form
    {
        public fAdmin_Cus()
        {
            InitializeComponent();
        }

        #region atri
        string sta = "";
        int Index = -1;

        #endregion


        #region Method
        void createCol()
        {
            // Xóa các cột cũ (nếu có)
            dtgvCustomer.Columns.Clear();

            var colUserID = new DataGridViewTextBoxColumn
            {
                Name = "colUserID",
                DataPropertyName = "UserId",
                HeaderText = "ID",
            };

            var colUserName = new DataGridViewTextBoxColumn
            {
                Name = "colUserName",
                DataPropertyName = "Username",
                HeaderText = "Tên đăng nhập",
            };

            var colName = new DataGridViewTextBoxColumn
            {
                Name = "colName",
                DataPropertyName = "Name",
                HeaderText = "Tên",
            };

            var colEmail = new DataGridViewTextBoxColumn
            {
                Name = "colEmail",
                DataPropertyName = "Email",
                HeaderText = "Email",
            };

            var colPhone = new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                DataPropertyName = "Phone",
                HeaderText = "Số điện thoại",
            };

            var colPassword = new DataGridViewTextBoxColumn
            {
                Name = "colPassword",
                DataPropertyName = "Password",
                HeaderText = "Mật khẩu",
            };

            dtgvCustomer.Columns.AddRange(new DataGridViewColumn[] { colUserID, colUserName, colName, colEmail, colPhone, colPassword });
            dtgvCustomer.RowHeadersVisible = false;
            dtgvCustomer.AllowUserToResizeColumns = false;
            dtgvCustomer.AllowUserToResizeRows = false;
            dtgvCustomer.RowTemplate.Height = 35;
            dtgvCustomer.ColumnHeadersHeight = 40;
            dtgvCustomer.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dtgvCustomer.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgvCustomer.EnableHeadersVisualStyles = false;
            dtgvCustomer.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 11F, FontStyle.Bold);
            dtgvCustomer.DefaultCellStyle.Font = new Font("Times New Roman", 10F, FontStyle.Regular);
            dtgvCustomer.RowsDefaultCellStyle.BackColor = Color.White;
            dtgvCustomer.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        void clear_panel()
        {
            foreach (var c in panel1.Controls)
            {
                if (c is System.Windows.Forms.TextBox textBox) // Fully qualify 'TextBox' to resolve ambiguity  
                {
                    textBox.Clear();
                }
            }
        }

        void button_isEnable(bool t_s_x, bool h_l)
        {
            button1.Enabled = t_s_x;
            button2.Enabled = t_s_x;
            button3.Enabled = t_s_x;
            button4.Enabled = h_l;
            button5.Enabled = h_l;
        }
        void LoadCustome()
        {
            dtgvCustomer.DataSource = null;
            createCol(); // Gọi createCol trước khi gán DataSource
            dtgvCustomer.DataSource = BUS_Customer.Instance.GetAllCustomer();
            dtgvCustomer.Refresh();

            // Cập nhật số lượng khách hàng trong lbcount
            lbcount.Text = BUS_Customer.Instance.CountCustomer().ToString();
        }

        private void dtgvCustomer_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            Index = e.RowIndex;
        }

        void isEnable(bool tb, bool dtgv)
        {
            textBox1.Enabled = tb;
            textBox2.Enabled = tb;
            textBox3.Enabled = tb;
            textBox4.Enabled = tb;
            textBox5.Enabled = tb;
            textBox6.Enabled = tb;
            dtgvCustomer.Enabled = dtgv;
        }
        bool check_null_panel()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)
                || string.IsNullOrWhiteSpace(textBox2.Text)
                || string.IsNullOrWhiteSpace(textBox3.Text)
                || string.IsNullOrWhiteSpace(textBox4.Text)
                || string.IsNullOrWhiteSpace(textBox5.Text)
                || string.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }
            return true;
        }
        bool check_SDT(string phoneNumber)
        {
            string cleanedNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (cleanedNumber.Length == 10 && long.TryParse(cleanedNumber, out long number) && number >= 0)
            {
                return true;
            }

            return false;
        }
        string check_email(string email)
        {
            // Kiểm tra null hoặc rỗng
            if (string.IsNullOrWhiteSpace(email))
            {
                return email;
            }

            // Loại bỏ khoảng trắng ở đầu và cuối
            email = email.Trim();

            // Kiểm tra xem email đã chứa "@" hay chưa
            if (!email.Contains("@"))
            {
                // Nếu không có "@", thêm "@gmail.com"
                return email + "@gmail.com";
            }
            else if (!email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                // Nếu có "@" nhưng không kết thúc bằng "@gmail.com", thay thế phần đuôi
                int atIndex = email.IndexOf("@");
                return email.Substring(0, atIndex) + "@gmail.com";
            }

            // Nếu đã có "@gmail.com", trả về nguyên gốc
            return email;
        }
        #endregion

        #region event
        private void fAdmin_Cus_Load(object sender, EventArgs e)
        {
            isEnable(false, true);
            button_isEnable(true, false);
            LoadCustome();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isEnable(true, false);
            button_isEnable(false, true);
            sta = "add";
            if (BUS_Customer.Instance.CountCustomer() < 100)
                textBox1.Text = "U0" + (BUS_Customer.Instance.CountCustomer() + 1).ToString();
            else
                textBox1.Text = "U" + (BUS_Customer.Instance.CountCustomer() + 1).ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            textBox2.ReadOnly = true;
            button_isEnable(false, true);
            textBox1.Text = BUS_Customer.Instance.GetAllCustomer()[Index].UserId;
            textBox2.Text = BUS_Customer.Instance.GetAllCustomer()[Index].UserName;
            textBox3.Text = BUS_Customer.Instance.GetAllCustomer()[Index].Name;
            textBox4.Text = BUS_Customer.Instance.GetAllCustomer()[Index].Email;
            textBox5.Text = BUS_Customer.Instance.GetAllCustomer()[Index].Phone;
            textBox6.Text = BUS_Customer.Instance.GetAllCustomer()[Index].Password;
            isEnable(true, false);
            sta = "edit";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng thuộc bảng để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Xác nhận xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) { return; }

            BUS_Customer.Instance.DeleteCustomer(Index);
            LoadCustome();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            button_isEnable(true, false);
            clear_panel();
            isEnable(false, true);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = false;
            if (!check_null_panel())
            {
                return;
            }
            if (!check_SDT(textBox4.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập lại.", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.Focus();
                return;
            }
            textBox3.Text = check_email(textBox3.Text);
            string username = textBox2.Text; // Giả sử username = name nếu không có textBox6
            if (sta == "add")
            {
                BUS_Customer.Instance.AddCustomer(new User(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text));
            }
            else if (sta == "edit")
            {
                if (Index < 0)
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để sửa đổi.");
                    return;
                }
                BUS_Customer.Instance.UpdateCustomer(new User(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text), Index);
            }
            button_isEnable(true, false);
            LoadCustome();
            clear_panel();
            isEnable(false, true);
        }

        #endregion
    }
}
