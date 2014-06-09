using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MonthCalendar;

namespace SPFA
{
    /// <summary>A very quick and dirty WPF Month-view calendar control that supports simple 1-day appointments.
    /// This is *NOT* meant to showcase best practices for WPF, or for .Net coding in general.  Please improve it, and post the
    /// improvements to CodeProject so that others can benefit, thanks!  Kirk Davis, February 2009, Bangkok, Thailand.
    /// </summary>
    /// <remarks>
    /// ''' This code is for anybody to use for any legal reason.  Given that I wrote this in about four hours, use it at your own risk.
    /// If your application crashes, a memory-leak brings down your entire country, or you hate it, you take full responsibility.</remarks>
    public partial class Calendar : Window
    {
        public Calendar(User cu)
        {
            currentUser = cu;
            InitializeComponent();
            Loaded += Window1_Loaded;
        }
                private User currentUser;
                private List<Appointment> _myAppointmentsList;
                private int eventCounter;

                //Accessors and mutators
                public List<Appointment> MyAppointmentsList
                {
                    get { return _myAppointmentsList; }
                    set { _myAppointmentsList = value; }
                }
                
                //Accessors and mutators
                public int EventCounter
                {
                    get { return eventCounter; }
                    set { eventCounter = value; }
                }
        /*
         * 
         * This method is used to load any events already associated with the user into the calendar. This is run at the start of the calendar window
         */
        private void Window1_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (currentUser.UserEvents.Count == 0)
            {
                Appointment apt = new Appointment();
                apt.AppointmentID = 0;
                apt.StartTime = DateTime.Today;
                apt.EndTime = DateTime.Today;
                apt.Subject = "Test appointment";
                currentUser.UserEvents.Add(apt);
                eventCounter++;
            }
            try
            {
                _myAppointmentsList = currentUser.UserEvents;
                eventCounter = currentUser.UserEvents.Count;
                SetAppointments();
            }
            catch
            {
                //Error handling
            }


        }

        /*
         * This method is activated when a user double clicks on a date in the calendar
         * This opens up the window to create a new event
         * 
         * */
        private void DayBoxDoubleClicked_event(NewAppointmentEventArgs e)
        {
            EventWindow ew = new EventWindow(this, e);
            ew.Show();
           
        }

        /**
         * This method is used when a user double clicks on an event in the calendar
         * This removes the event from the calendar and removes its association with the user
         * 
         * */
        private void AppointmentDblClicked(int Appointment_Id)
        {
            Boolean isFound = false;
            for(int i = 0; i < eventCounter && !isFound; i++)
            {
                if(_myAppointmentsList.ElementAt(i).AppointmentID == Appointment_Id)
                {
                    Appointment_Id = i;
                    isFound = true;
                }
            }

            _myAppointmentsList.Remove(_myAppointmentsList.ElementAt(Appointment_Id));
            eventCounter--;
            SetAppointments();
        }

        /*
         * This keeps the events on the calendar when a user tries to view other months
         * */
        private void DisplayMonthChanged(MonthChangedEventArgs e)
        {
            SetAppointments();
        }

        /*
         * This method adds all events associated with the user into the calendar so they will be displayed visually
         * */
        public void SetAppointments()
        {
            AptCalendar.MonthAppointments = _myAppointmentsList.FindAll(new System.Predicate<Appointment>((Appointment apt) => apt.StartTime != null && Convert.ToDateTime(apt.StartTime).Month == this.AptCalendar.DisplayStartDate.Month && Convert.ToDateTime(apt.StartTime).Year == this.AptCalendar.DisplayStartDate.Year));
        }

        /*
         * This closes the current window
         * */
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }

