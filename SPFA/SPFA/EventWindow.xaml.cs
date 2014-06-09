using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonthCalendar;

namespace SPFA
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        public EventWindow(Calendar c, NewAppointmentEventArgs d)
        {
            cal = c;
            this.d = d;
            InitializeComponent();
        }

        Calendar cal;
        NewAppointmentEventArgs d;

        /*
         * Clicking on this button creates a new event on the calendar based on the information typed in the textbox
         * */
        private void info_Button_Click(object sender, RoutedEventArgs e)
        {
            Appointment apt = new Appointment();
            apt.AppointmentID = cal.EventCounter;
            apt.StartTime = d.StartDate;
            apt.EndTime = apt.StartTime;
            apt.Subject = Infobox.Text;
            cal.MyAppointmentsList.Add(apt);
            cal.EventCounter++;
            cal.SetAppointments();
            Infobox.Text = "Enter here";
            this.Close();
        }

        //Clears text in textbox when clicked
        private void Infobox_GotFocus(object sender, RoutedEventArgs e)
        {
            Infobox.Text = string.Empty;
        }

    }
}
