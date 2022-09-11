using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ProjektyMG
{
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
            Read();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ModelList.Items.Clear();
        }
        /// <summary>
        /// a method that forbids inputting anything other than a number 
        /// </summary>
        /// <param></param>
        /// <returns>Can't insert string</returns>
        private void PriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e) => Create();

        private void UpdateButton_Click(object sender, RoutedEventArgs e) => Update();

        private void DeleteButton_Click(object sender, RoutedEventArgs e) => Delete();
        public void Read()
        {
            using (DbAplicationContext context = new DbAplicationContext())
            {
                ModelList.ItemsSource = context.ModelBike.ToList();
            }

        }
        /// <summary>
        /// a method that Create new object to table
        /// </summary>
        /// <param></param>
        /// <returns>Create new object</returns>
        public void Create()
        {

            using (DbAplicationContext context = new DbAplicationContext())
            {
                var mark = MarkTextBox.Text;
                var wheleSize = WheleSizeTextBox.Text;
                var numberGear = NumberGearTextBox.Text;

                if (mark == null || wheleSize== null || numberGear == null)
                {
                    MessageBox.Show("Wprowadz dane");
                }
                else
                {
                    context.ModelBike.Add(new ModelBike() { Mark = mark, WheelSize = int.Parse(wheleSize), NumberGears= int.Parse(numberGear) });
                    context.SaveChanges();
                }
                Read();
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
                ModelBike selectedModelBike = ModelList.SelectedItem as ModelBike;

                var mark = MarkTextBox.Text;
                var wheleSize = WheleSizeTextBox.Text;
                var numberGear = NumberGearTextBox.Text;

                if (mark != null || wheleSize != null || numberGear != null)
                {
                    ModelBike model = context.ModelBike.Find(selectedModelBike.ID);
                    model.Mark = mark;
                    model.WheelSize = int.Parse(wheleSize);
                    model.NumberGears = int.Parse(numberGear);

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
                ModelBike selectedModelBike = ModelList.SelectedItem as ModelBike;

                if (selectedModelBike != null)
                {
                    ModelBike user = context.ModelBike.Single(x => x.ID == selectedModelBike.ID);

                    context.Remove(user);
                    context.SaveChanges();
                }
            }
            Read();
        }
        /// <summary>
        /// a Evet that close Admin Panel and open Login window
        /// </summary>
        /// <param></param>
        /// <returns>closeing admin panel and open login window</returns>
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            login Dashboard = new();
            Dashboard.Show();
            this.Close();
        }
    }
}
