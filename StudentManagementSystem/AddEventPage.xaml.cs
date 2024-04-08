using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentManagementSystem.Pages
{
    public partial class AddEventPage : Page
    {
        public event EventHandler<Event> EventAdded;

        public AddEventPage()
        {
            InitializeComponent();
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            DateTime eventDate = EventDatePicker.SelectedDate ?? DateTime.Now;
            string description = DescriptionTextBox.Text;

            Event newEvent = new Event { Date = eventDate, Description = description };

            EventAdded?.Invoke(this, newEvent);
        }
    }
}