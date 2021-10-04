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
                labelDisplay.Content = dateID.Text;
                CountDownDisplay(daysToDate);
            }
            dateID.Text = "";
        }

        private void recentDates_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (recentDates.HasItems)
            {
                dynamic selectedItemRow = recentDates.SelectedItem;
                DateTime selectedItemDate = (DateTime)DateTime.Parse(selectedItemRow.Date);
                string selectedItemLabel = selectedItemRow.Label;
                int daysToDate = (selectedItemDate - DateTime.Today).Days;
                labelDisplay.Content = selectedItemLabel;
                selectedDate = selectedItemDate;
                CountDownDisplay(daysToDate);
            }
        }

        private void CountDownDisplay(int days)
        {
            string formattedDate = selectedDate.ToString("MMMM d, yyyy");
            if (days >= 0)
            {
                switch (days)
                {
                    case 0:
                        countdownText.Content = "";
                        countdownDisplay.Content = "Today";
                        break;
                    case 1:
                        countdownText.Content = "to " + formattedDate;
                        countdownDisplay.Content = days + " Day";
                        break;
                    default:
                        countdownText.Content = "to " + formattedDate;
                        countdownDisplay.Content = days + " Days";
                        break;
                }
            }
            else
            {
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
