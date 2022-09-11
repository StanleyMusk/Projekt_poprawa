using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace ProjektyMG
{
    public partial class Reservation : Window
    {
        public Reservation()
        {
            InitializeComponent();
            Read();
            ReadWorkers();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ItemList.Items.Clear();
        }
        private void MenuWorkers_Click(object sender, RoutedEventArgs e)
        {
            ListWorker.Items.Clear();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e) => Create();

        private void UpdateButton_Click(object sender, RoutedEventArgs e) => Update();  

        private void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
        /// <summary>
        /// a method that Create new object to table
        /// </summary>
        /// <param></param>
        /// <returns>Create new object</returns>
        public void Create()
        {

            using (DbAplicationContext context = new DbAplicationContext())
            {
                var name = NameTextBox.Text;
                var price = PriceTextBox.Text;
                var whoPrepared = WhoPreparedTextBox.Text;

                if (name == null || price == null || whoPrepared == null)
                {
                    MessageBox.Show("Wprowadz dane");
                }
                else
                {
                    context.Bike.Add(new Bike() { Name = name, Price = double.Parse(price), WhoPrepared = long.Parse(whoPrepared) });
                    context.SaveChanges();                  
                }
                Read();
            }
        }

        public void Read()
        {
            using (DbAplicationContext context = new DbAplicationContext())
            {
                ItemList.ItemsSource = context.Bike.ToList();
            }

        }
        public void ReadWorkers()
        {
            using (DbAplicationContext context = new DbAplicationContext())
            {
                ListWorker.ItemsSource = context.Workers.ToList();
            }

        }
        /// <summary>
        /// a method that Update seleced row in table
        /// </summary>
        /// <param></param>
        /// <returns>Update row</returns>
        public void Update()
        {
            using (DbAplicationContext context = new DbAplicationContext())
            {
                Bike selectedUser = ItemList.SelectedItem as Bike;

                var name = NameTextBox.Text;
                var price = PriceTextBox.Text;
                var whoPrepared = WhoPreparedTextBox.Text;

                if (name != null && PriceTextBox != null)
                {
                    Bike bike = context.Bike.Find(selectedUser.ID);
                    bike.Name = name;
                    bike.Price = double.Parse(price);
                    bike.WhoPrepared = long.Parse(whoPrepared);

                    context.SaveChanges();
                }
            }
            Read();
        }
        /// <summary>
        /// a method that Delete seleced row in table
        /// </summary>
        /// <param></param>
        /// <returns>Delete row</returns>
        public void Delete()
        {
            using (DbAplicationContext context = new DbAplicationContext())
            {
                Bike selectedUser = ItemList.SelectedItem as Bike;

                if (selectedUser != null)
                {
                    Bike user = context.Bike.Single(x => x.ID == selectedUser.ID);

                    context.Remove(user);
                    context.SaveChanges();
                }
            }
            Read();
        }

        private void OnlyNumber_Preview(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
        /// <summary>
        /// a method that forbids inputting anything other than a number 
        /// </summary>
        /// <param></param>
        /// <returns>Can't insert string</returns>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            login Dashboard = new();
            Dashboard.Show();
            this.Close();
        }
    }
}
