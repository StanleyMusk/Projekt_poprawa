using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace ProjektyMG
{
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Evet that log in system when corect loggin and password
        /// </summary>
        /// <param></param>
        /// <returns>log in system</returns>
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            var UserName = Username.Text;
            var PassWord = Password.Password;

            using (DbAplicationContext db = new())
            {
                bool userFound = db.Users.Any(user => user.Username == UserName && user.Password == PassWord);
                var query = from st in db.Users
                            where st.Username == UserName
                            select st;

                if (userFound)
                {
                    LogIn(UserName, PassWord);
                }
                else
                {
                    MessageBox.Show("Niepoprawny login lub hasło");
                }

            }
        }
        /// <summary>
        /// a Methot that select user and open specific windows
        /// </summary>
        /// <param name="users" name="password">data of login form</param>
        /// <returns>open user or admin window</returns>
        private void LogIn(string users, string password )
        {
            if(users == "admin" && password == "admin")
            {
                AdminPanel Dashboard = new();
                Dashboard.Show();
                this.Close();
            }
            else
            {
                Reservation Dashboard = new();
                Dashboard.Show();
                this.Close();
            }
        }
    }
}
