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

namespace Date_Countdown
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime selectedDate;
        public MainWindow()
        {
            InitializeComponent();
            recentDates.Items.Clear();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void setDateButton_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                selectedDate = (DateTime)datePicker.SelectedDate;
                recentDates.Items.Insert(0, (new { Label = dateID.Text, Date = selectedDate.ToString("MM/dd/yyy") } ));

                int daysToDate = (selectedDate - DateTime.Today).Days;
                CountDownDisplay(daysToDate);
            }
            dateID.Text = "";
        }

        private void recentDates_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (recentDates.HasItems)
            {
                dynamic selectedItemRow = recentDates.SelectedItem;
                DateTime selectedItem = (DateTime)DateTime.Parse(selectedItemRow.Date);
                int daysToDate = (selectedItem - DateTime.Today).Days;
                selectedDate = selectedItem;
                CountDownDisplay(daysToDate);
            }
        }

        private void CountDownDisplay(int days)
        {
            string formattedDate = selectedDate.ToString("MMMM d, yyyy");
            dynamic itemLabel = recentDates.Items.GetItemAt(0);
            if (days >= 0)
            {
                switch (days)
                {
                    case 0:
                        labelDisplay.Content = itemLabel.Label;
                        countdownText.Content = "";
                        countdownDisplay.Content = "Today";
                        break;
                    case 1:
                        labelDisplay.Content = itemLabel.Label;
                        countdownText.Content = "to " + formattedDate;
                        countdownDisplay.Content = days + " Day";
                        break;
                    default:
                        labelDisplay.Content = itemLabel.Label;
                        countdownText.Content = "to " + formattedDate;
                        countdownDisplay.Content = days + " Days";
                        break;
                }
            }
            else
            {
                labelDisplay.Content = itemLabel.Label;
                countdownText.Content = "past " + formattedDate;
                if (days == -1)
                {
                    countdownDisplay.Content = -days + " Day";
                }
                else
                {
                    countdownDisplay.Content = -days + " Days";
                }
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (recentDates.SelectedItem != null)
            {
                var selectedItem = recentDates.SelectedItem;
                recentDates.Items.Remove(selectedItem);
            }
        }

        private void dateID_KeyDown(object sender, KeyEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue && e.Key == Key.Enter)
            {
                DateTime selectedDate = (DateTime)datePicker.SelectedDate;
                recentDates.Items.Insert(0, (new { Label = dateID.Text, Date = selectedDate.ToString("MM/dd/yyy") }));

                int daysToDate = (selectedDate - DateTime.Today).Days;
                CountDownDisplay(daysToDate);
            }
        }

        private void dateID_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            dateID.HorizontalContentAlignment = HorizontalAlignment.Left;
            tb.GotFocus -= dateID_GotFocus;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
