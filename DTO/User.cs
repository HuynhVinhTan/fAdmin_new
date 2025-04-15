using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL_Tan.DTO
{
    class User
    {
        
        private string userId;
        private string userName;
        private string name;
        private string email;
        private string phone;
        private string password;

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Password { get => password; set => password = value; }
        public string UserId { get => userId; set => userId = value; }
        public string UserName { get => userName; set => userName = value; }

        public User(string userId, string username, string name, string email, string phone, string password)
        {
            this.userId = userId;
            this.userName = username;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.password = password;
        }
    }
}
